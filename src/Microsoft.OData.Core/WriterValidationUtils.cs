//---------------------------------------------------------------------
// <copyright file="WriterValidationUtils.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

namespace Microsoft.OData
{
    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Microsoft.OData.Core;
    using Microsoft.OData.Edm;
    using Microsoft.OData.Metadata;
    using Microsoft.VisualBasic;
    #endregion Namespaces

    /// <summary>
    /// Class with utility methods for validating OData content when writing.
    /// </summary>
    internal static class WriterValidationUtils
    {
        /// <summary>
        /// Validates that message writer settings are correct.
        /// </summary>
        /// <param name="messageWriterSettings">The message writer settings to validate.</param>
        /// <param name="writingResponse">True if we are writing a response.</param>
        internal static void ValidateMessageWriterSettings(ODataMessageWriterSettings messageWriterSettings, bool writingResponse)
        {
            Debug.Assert(messageWriterSettings != null, "messageWriterSettings != null");

            if (messageWriterSettings.BaseUri != null && !messageWriterSettings.BaseUri.IsAbsoluteUri)
            {
                throw new ODataException(Error.Format(SRResources.WriterValidationUtils_MessageWriterSettingsBaseUriMustBeNullOrAbsolute, UriUtils.UriToString(messageWriterSettings.BaseUri)));
            }

            if (messageWriterSettings.HasJsonPaddingFunction() && !writingResponse)
            {
                throw new ODataException(SRResources.WriterValidationUtils_MessageWriterSettingsJsonPaddingOnRequestMessage);
            }
        }

        /// <summary>
        /// Validates an <see cref="ODataProperty"/> for not being null.
        /// </summary>
        /// <param name="property">The property to validate for not being null.</param>
        internal static void ValidatePropertyNotNull(ODataPropertyInfo property)
        {
            if (property == null)
            {
                throw new ODataException(SRResources.WriterValidationUtils_PropertyMustNotBeNull);
            }
        }

        /// <summary>
        /// Validates a property name to ensure all required information is specified.
        /// </summary>
        /// <param name="propertyName">The property name to validate.</param>
        internal static void ValidatePropertyName(string propertyName)
        {
            // Properties must have a non-empty name
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ODataException(SRResources.WriterValidationUtils_PropertiesMustHaveNonEmptyName);
            }

            ValidationUtils.ValidatePropertyName(propertyName);
        }

        /// <summary>
        /// Validates that a property with the specified name exists on a given structured type.
        /// The structured type can be null if no metadata is available.
        /// </summary>
        /// <param name="propertyName">The name of the property to validate.</param>
        /// <param name="owningStructuredType">The owning type of the property with name <paramref name="propertyName"/>
        /// or null if no metadata is available.</param>
        /// <param name="throwOnUndeclaredProperty">Flag indicating whether undeclared property on non open type should be prohibited.</param>
        /// <returns>The <see cref="IEdmProperty"/> instance representing the property with name <paramref name="propertyName"/>
        /// or null if no metadata is available.</returns>
        internal static IEdmProperty ValidatePropertyDefined(
            string propertyName,
            IEdmStructuredType owningStructuredType,
            bool throwOnUndeclaredProperty)
        {
            Debug.Assert(!string.IsNullOrEmpty(propertyName), "!string.IsNullOrEmpty(propertyName)");

            if (owningStructuredType == null)
            {
                return null;
            }

            IEdmProperty property = owningStructuredType.FindProperty(propertyName);

            if (throwOnUndeclaredProperty && !owningStructuredType.IsOpen && property == null)
            {
                throw new ODataException(Error.Format(SRResources.ValidationUtils_PropertyDoesNotExistOnType, propertyName, owningStructuredType.FullTypeName()));
            }

            return property;
        }

        /// <summary>
        /// Validates that the property given is defined.
        /// </summary>
        /// <param name="propertyInfo">The info of property.</param>
        /// <param name="throwOnUndeclaredProperty">Whether undeclared property on non open type should be prohibited.</param>
        internal static void ValidatePropertyDefined(PropertySerializationInfo propertyInfo, bool throwOnUndeclaredProperty)
        {
            if (propertyInfo.MetadataType.OwningType == null)
            {
                return;
            }

            if (throwOnUndeclaredProperty && propertyInfo.MetadataType.IsUndeclaredProperty && !propertyInfo.MetadataType.IsOpenProperty)
            {
                throw new ODataException(Error.Format(SRResources.ValidationUtils_PropertyDoesNotExistOnType, propertyInfo.PropertyName, propertyInfo.MetadataType.OwningType.FullTypeName()));
            }
        }

        /// <summary>
        /// Validates that a navigation property with the specified name exists on a given entity type.
        /// The entity type can be null if no metadata is available.
        /// </summary>
        /// <param name="propertyName">The name of the property to validate.</param>
        /// <param name="owningType">The owning entity type/complex type or null if no metadata is available.</param>
        /// <param name="throwOnUndeclaredProperty">Flag indicating whether undeclared property on non open type should be prohibited.</param>
        /// <returns>The <see cref="IEdmProperty"/> instance representing the navigation property with name <paramref name="propertyName"/>
        /// or null if no metadata is available.</returns>
        internal static IEdmNavigationProperty ValidateNavigationPropertyDefined(string propertyName, IEdmStructuredType owningType, bool throwOnUndeclaredProperty)
        {
            Debug.Assert(!string.IsNullOrEmpty(propertyName), "!string.IsNullOrEmpty(propertyName)");

            if (owningType == null)
            {
                return null;
            }

            IEdmProperty property = ValidatePropertyDefined(propertyName, owningType, throwOnUndeclaredProperty);
            if (property == null)
            {
                return null;
            }

            if (property.PropertyKind != EdmPropertyKind.Navigation)
            {
                // The property must be a navigation property
                throw new ODataException(Error.Format(SRResources.ValidationUtils_NavigationPropertyExpected, propertyName, owningType.FullTypeName(), property.PropertyKind.ToString()));
            }

            return (IEdmNavigationProperty)property;
        }

        /// <summary>
        /// Validates a resource in an expanded link to make sure the entity types match.
        /// </summary>
        /// <param name="resourceType">The <see cref="IEdmEntityType"/> of the resource.</param>
        /// <param name="parentNavigationPropertyType">The type of the parent navigation property.</param>
        internal static void ValidateNestedResource(IEdmStructuredType resourceType, IEdmStructuredType parentNavigationPropertyType)
        {
            if (parentNavigationPropertyType == null)
            {
                return;
            }

            Debug.Assert(resourceType != null, "If we have a parent navigation property type we should also have a resource type.");

            // Make sure the entity types are compatible
            if (!parentNavigationPropertyType.IsAssignableFrom(resourceType))
            {
                throw new ODataException(Error.Format(SRResources.WriterValidationUtils_NestedResourceTypeNotCompatibleWithParentPropertyType, resourceType.FullTypeName(), parentNavigationPropertyType.FullTypeName()));
            }
        }

        /// <summary>
        /// Validates that an <see cref="ODataOperation"/> can be written.
        /// </summary>
        /// <param name="operation">The operation (an action or a function) to validate.</param>
        /// <param name="writingResponse">true if writing a response; otherwise false.</param>
        internal static void ValidateCanWriteOperation(ODataOperation operation, bool writingResponse)
        {
            Debug.Assert(operation != null, "operation != null");

            // Operations are only valid in responses; we fail on them in requests
            if (!writingResponse)
            {
                throw new ODataException(Error.Format(SRResources.WriterValidationUtils_OperationInRequest, operation.Metadata));
            }
        }

        /// <summary>
        /// Validates an <see cref="ODataResourceSet"/> to ensure all required information is specified and valid on the WriteEnd call.
        /// </summary>
        /// <param name="resourceSet">The resource set to validate.</param>
        /// <param name="writingRequest">Flag indicating whether the resource set is written as part of a request or a response.</param>
        internal static void ValidateResourceSetAtEnd(ODataResourceSet resourceSet, bool writingRequest)
        {
            Debug.Assert(resourceSet != null, "resourceSet != null");

            // Verify next link
            if (resourceSet.NextPageLink != null)
            {
                // Check that NextPageLink is not set for requests
                if (writingRequest)
                {
                    throw new ODataException(SRResources.WriterValidationUtils_NextPageLinkInRequest);
                }
            }
        }

        /// <summary>
        /// Validates an <see cref="ODataDeltaResourceSet"/> to ensure all required information is specified and valid on the WriteEnd call.
        /// </summary>
        /// <param name="resourceSet">The resource set to validate.</param>
        /// <param name="writingRequest">Flag indicating whether the resource set is written as part of a request or a response.</param>
        internal static void ValidateDeltaResourceSetAtEnd(ODataDeltaResourceSet resourceSet, bool writingRequest)
        {
            Debug.Assert(resourceSet != null, "resourceSet != null");

            // Verify next link
            if (resourceSet.NextPageLink != null)
            {
                // Check that NextPageLink is not set for requests
                if (writingRequest)
                {
                    throw new ODataException(SRResources.WriterValidationUtils_NextPageLinkInRequest);
                }
            }
        }

        /// <summary>
        /// Validates an <see cref="ODataResourceBase"/> to ensure all required information is specified and valid on WriteStart call.
        /// </summary>
        /// <param name="resource">The resource to validate.</param>
        internal static void ValidateResourceAtStart(ODataResourceBase resource)
        {
            Debug.Assert(resource != null, "resource != null");

            // Id can be specified at the beginning (and might be written here), so we need to validate it here.
            ValidateResourceId(resource.Id);

            // Type name is verified in the format readers/writers since it's shared with other non-entity types
            // and verifying it here would mean doing it twice.
        }

        /// <summary>
        /// Validates an <see cref="ODataResourceBase"/> to ensure all required information is specified and valid on WriteEnd call.
        /// </summary>
        /// <param name="resource">The resource to validate.</param>
        internal static void ValidateResourceAtEnd(ODataResourceBase resource)
        {
            Debug.Assert(resource != null, "resource != null");

            // If the Id was not specified in the beginning it might have been specified at the end, so validate it here as well.
            ValidateResourceId(resource.Id);
        }

        /// <summary>
        /// Validates an <see cref="ODataStreamReferenceValue"/> to ensure all required information is specified and valid.
        /// </summary>
        /// <param name="streamReference">The stream reference to validate.</param>
        /// <param name="isDefaultStream">true if <paramref name="streamReference"/> is the default stream for an entity; false if it is a named stream property value.</param>
        internal static void ValidateStreamReferenceValue(ODataStreamReferenceValue streamReference, bool isDefaultStream)
        {
            Debug.Assert(streamReference != null, "streamReference != null");

            if (streamReference.ContentType != null && streamReference.ContentType.Length == 0)
            {
                throw new ODataException(SRResources.WriterValidationUtils_StreamReferenceValueEmptyContentType);
            }

            if (isDefaultStream && streamReference.ReadLink == null && streamReference.ContentType != null)
            {
                throw new ODataException(SRResources.WriterValidationUtils_DefaultStreamWithContentTypeWithoutReadLink);
            }

            if (isDefaultStream && streamReference.ReadLink != null && streamReference.ContentType == null)
            {
                throw new ODataException(SRResources.WriterValidationUtils_DefaultStreamWithReadLinkWithoutContentType);
            }

            // Default stream can be completely empty (no links or anything)
            // that is used to effectively mark the resource as MLE without providing any MR information.
            // OData clients when creating new MLE/MR might not have the MR information (yet) when sending the first PUT, but they still
            // need to mark the resource as MLE so that properties are written out-of-content. In such scenario the client can just set an empty
            // default stream to mark the resource as MLE.
            // That will cause the writer to write the properties outside the content without producing any content element.
            if (streamReference.EditLink == null && streamReference.ReadLink == null && !isDefaultStream)
            {
                throw new ODataException(SRResources.WriterValidationUtils_StreamReferenceValueMustHaveEditLinkOrReadLink);
            }

            if (streamReference.EditLink == null && streamReference.ETag != null)
            {
                throw new ODataException(SRResources.WriterValidationUtils_StreamReferenceValueMustHaveEditLinkToHaveETag);
            }
        }

        /// <summary>
        /// Validates a named stream property to ensure it's not null and it's name if correct.
        /// </summary>
        /// <param name="streamPropertyInfo">The stream reference property to validate.</param>
        /// <param name="edmProperty">Property metadata to validate against.</param>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="writingResponse">true when writing a response; otherwise false.</param>
        /// <remarks>This does NOT validate the value of the stream property, just the property itself.</remarks>
        internal static void ValidateStreamPropertyInfo(IODataStreamReferenceInfo streamPropertyInfo, IEdmProperty edmProperty, string propertyName, bool writingResponse)
        {
            Debug.Assert(streamPropertyInfo != null, "streamProperty != null");

            ValidationUtils.ValidateStreamPropertyInfo(streamPropertyInfo, edmProperty, propertyName);

            if (!writingResponse)
            {
                // Read/Write links and ETags on Stream properties are only valid in responses; writers fail if they encounter them in requests.
                if (streamPropertyInfo != null && streamPropertyInfo.EditLink != null || streamPropertyInfo.ReadLink != null || streamPropertyInfo.ETag != null)
                {
                    throw new ODataException(Error.Format(SRResources.WriterValidationUtils_StreamPropertyInRequest, propertyName));
                }
            }
        }

        /// <summary>
        /// Validates the value type of a property meets the derived type constraints.
        /// </summary>
        /// <param name="propertySerializationInfo">The property serialization info.</param>
        /// <remarks>This does NOT validate the value of the property, just the type of property.</remarks>
        internal static void ValidatePropertyDerivedTypeConstraint(PropertySerializationInfo propertySerializationInfo)
        {
            Debug.Assert(propertySerializationInfo != null, "propertySerializationInfo != null");

            // Skip the undeclared property
            if (propertySerializationInfo.MetadataType.IsUndeclaredProperty)
            {
                return;
            }

            PropertyValueTypeInfo valueType = propertySerializationInfo.ValueType;
            if (valueType == null || valueType.TypeReference == null)
            {
                return;
            }

            // make sure the same type can pass the validation.
            if (propertySerializationInfo.MetadataType.TypeReference.Definition == valueType.TypeReference.Definition)
            {
                return;
            }

            string fullTypeName = valueType.TypeReference.FullName();
            if (propertySerializationInfo.MetadataType.DerivedTypeConstraints == null)
            {
                return;
            }

            // this runs in a hot path, hence the use of a loop instead of LINQ to avoid allocations
            foreach (string d in propertySerializationInfo.MetadataType.DerivedTypeConstraints)
            {
                if (d == fullTypeName)
                {
                    return;
                }
            }

            throw new ODataException(Error.Format(SRResources.WriterValidationUtils_ValueTypeNotAllowedInDerivedTypeConstraint, fullTypeName, "property", propertySerializationInfo.PropertyName));
        }

        /// <summary>
        /// Validates that the specified <paramref name="entityReferenceLink"/> is not null.
        /// </summary>
        /// <param name="entityReferenceLink">The entity reference link to validate.</param>
        /// <remarks>This should be called only for entity reference links inside the ODataEntityReferenceLinks.Links collection.</remarks>
        internal static void ValidateEntityReferenceLinkNotNull(ODataEntityReferenceLink entityReferenceLink)
        {
            if (entityReferenceLink == null)
            {
                throw new ODataException(SRResources.WriterValidationUtils_EntityReferenceLinksLinkMustNotBeNull);
            }
        }

        /// <summary>
        /// Validates an entity reference link instance.
        /// </summary>
        /// <param name="entityReferenceLink">The entity reference link to validate.</param>
        internal static void ValidateEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink)
        {
            Debug.Assert(entityReferenceLink != null, "entityReferenceLink != null");

            if (entityReferenceLink.Url == null)
            {
                throw new ODataException(SRResources.WriterValidationUtils_EntityReferenceLinkUrlMustNotBeNull);
            }
        }

        /// <summary>
        /// Validates an <see cref="ODataNestedResourceInfo"/> to ensure all required information is specified and valid.
        /// </summary>
        /// <param name="nestedResourceInfo">The nested resource info to validate.</param>
        /// <param name="declaringStructuredType">The <see cref="IEdmStructuredType"/> declaring the structural property or navigation property; or null if metadata is not available.</param>
        /// <param name="expandedPayloadKind">The <see cref="ODataPayloadKind"/> of the expanded content of this nested resource info or null for deferred links.</param>
        /// <param name="throwOnUndeclaredProperty">Flag indicating whether undeclared property on non open type should be prohibited.</param>
        /// <returns>The type of the navigation property for this nested resource info; or null if no <paramref name="declaringStructuredType"/> was specified.</returns>
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Keeping the validation code for nested resource info multiplicity in one place.")]
        internal static IEdmNavigationProperty ValidateNestedResourceInfo(
            ODataNestedResourceInfo nestedResourceInfo,
            IEdmStructuredType declaringStructuredType,
            ODataPayloadKind? expandedPayloadKind,
            bool throwOnUndeclaredProperty)
        {
            Debug.Assert(nestedResourceInfo != null, "nestedResourceInfo != null");
            Debug.Assert(
                !expandedPayloadKind.HasValue ||
                expandedPayloadKind.Value == ODataPayloadKind.EntityReferenceLink ||
                expandedPayloadKind.Value == ODataPayloadKind.Resource ||
                expandedPayloadKind.Value == ODataPayloadKind.ResourceSet,
                "If an expanded payload kind is specified it must be resource, resource set or entity reference link.");

            // Navigation link must have a non-empty name
            if (string.IsNullOrEmpty(nestedResourceInfo.Name))
            {
                throw new ODataException(SRResources.ValidationUtils_LinkMustSpecifyName);
            }

            // If we write an entity reference link, don't validate the multiplicity of the IsCollection
            // property if it is 'false' (since we allow writing a singleton navigation link for
            // a collection navigation property in requests) nor the consistency of payload kind and metadata
            // (which is done separately in ODataWriterCore.CheckForNestedResourceInfoWithContent).
            bool isEntityReferenceLinkPayload = expandedPayloadKind == ODataPayloadKind.EntityReferenceLink;

            // true only if the expandedPayloadKind has a value and the value is 'Resource Set'
            bool isResourceSetPayload = expandedPayloadKind == ODataPayloadKind.ResourceSet;

            // Make sure the IsCollection property agrees with the payload kind for resource and resource set payloads
            Func<object, string> errorTemplate = null;
            if (!isEntityReferenceLinkPayload && nestedResourceInfo.IsCollection.HasValue && expandedPayloadKind.HasValue)
            {
                // For resource set/resource make sure the IsCollection property is set correctly.
                if (isResourceSetPayload != nestedResourceInfo.IsCollection.Value)
                {
                    errorTemplate = expandedPayloadKind.Value == ODataPayloadKind.ResourceSet
                        ? (object obj) => Error.Format(SRResources.WriterValidationUtils_ExpandedLinkIsCollectionFalseWithResourceSetContent, obj)
                        : (object obj) => Error.Format(SRResources.WriterValidationUtils_ExpandedLinkIsCollectionTrueWithResourceContent, obj);
                }
            }

            IEdmNavigationProperty navigationProperty = null;
            if (errorTemplate == null && declaringStructuredType != null)
            {
                navigationProperty = ValidateNavigationPropertyDefined(nestedResourceInfo.Name, declaringStructuredType, throwOnUndeclaredProperty);
                if (navigationProperty != null)
                {
                    bool isCollectionType = navigationProperty.Type.TypeKind() == EdmTypeKind.Collection;

                    // Make sure the IsCollection property agrees with the metadata type for resource and resource set payloads
                    if (nestedResourceInfo.IsCollection.HasValue && isCollectionType != nestedResourceInfo.IsCollection)
                    {
                        // Ignore the case where IsCollection is 'false' and we are writing an entity reference link
                        // (see comment above)
                        if (!(nestedResourceInfo.IsCollection == false && isEntityReferenceLinkPayload))
                        {
                            errorTemplate = isCollectionType
                                ? (object obj) => Error.Format(SRResources.WriterValidationUtils_ExpandedLinkIsCollectionFalseWithResourceSetMetadata, obj)
                                : (object obj) => Error.Format(SRResources.WriterValidationUtils_ExpandedLinkIsCollectionTrueWithResourceMetadata, obj);
                        }
                    }

                    // Make sure that the payload kind agrees with the metadata.
                    // For entity reference links we check separately in ODataWriterCore.CheckForNestedResourceInfoWithContent.
                    if (!isEntityReferenceLinkPayload && expandedPayloadKind.HasValue && isCollectionType != isResourceSetPayload)
                    {
                        errorTemplate = isCollectionType
                            ? (object obj) => Error.Format(SRResources.WriterValidationUtils_ExpandedLinkWithResourcePayloadAndResourceSetMetadata, obj)
                            : (object obj) => Error.Format(SRResources.WriterValidationUtils_ExpandedLinkWithResourceSetPayloadAndResourceMetadata, obj);
                    }
                }
            }

            if (errorTemplate != null)
            {
                string uri = nestedResourceInfo.Url == null ? "null" : UriUtils.UriToString(nestedResourceInfo.Url);
                throw new ODataException(errorTemplate(uri));
            }

            return navigationProperty;
        }

        /// <summary>
        /// Validates the input <see cref="IEdmStructuredType"/> meets the derived type constraints on the odata item.
        /// </summary>
        /// <param name="resourceType">The input resource type.</param>
        /// <param name="metadataType">The type from metadata.</param>
        /// <param name="derivedTypeConstraints">The derived type constraints on the nested resource.</param>
        /// <param name="itemKind">The item kind.</param>
        /// <param name="itemName">The item name.</param>
        internal static void ValidateDerivedTypeConstraint(IEdmStructuredType resourceType,
            IEdmStructuredType metadataType, IEnumerable<string> derivedTypeConstraints, string itemKind, string itemName)
        {
            if (resourceType == null || metadataType == null || derivedTypeConstraints == null || resourceType == metadataType)
            {
                return;
            }

            string fullTypeName = resourceType.FullTypeName();
            // this runs in a hot path, hence the use of a loop instead of LINQ to avoid allocations
            foreach (string c in derivedTypeConstraints)
            {
                if (c == fullTypeName)
                {
                    return;
                }
            }

            throw new ODataException(Error.Format(SRResources.WriterValidationUtils_ValueTypeNotAllowedInDerivedTypeConstraint, fullTypeName, itemKind, itemName));
        }

        /// <summary>
        /// Validates that the specified nested resource info has cardinality, that is it has the IsCollection value set.
        /// </summary>
        /// <param name="nestedResourceInfo">The nested resource info to validate.</param>
        internal static void ValidateNestedResourceInfoHasCardinality(ODataNestedResourceInfo nestedResourceInfo)
        {
            Debug.Assert(nestedResourceInfo != null, "nestedResourceInfo != null");

            if (!nestedResourceInfo.IsCollection.HasValue)
            {
                throw new ODataException(Error.Format(SRResources.WriterValidationUtils_NestedResourceInfoMustSpecifyIsCollection, nestedResourceInfo.Name));
            }
        }

        /// <summary>
        /// Validates that the expected property allows null value.
        /// </summary>
        /// <param name="expectedPropertyTypeReference">The expected property type or null if we don't have any.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="model">The model to use to get the OData version.</param>
        internal static void ValidateNullPropertyValue(IEdmTypeReference expectedPropertyTypeReference, string propertyName, IEdmModel model)
        {
            Debug.Assert(model != null, "For null validation, model is required.");

            if (expectedPropertyTypeReference != null)
            {
                if (expectedPropertyTypeReference.IsNonEntityCollectionType())
                {
                    throw new ODataException(Error.Format(SRResources.WriterValidationUtils_CollectionPropertiesMustNotHaveNullValue, propertyName));
                }

                if (expectedPropertyTypeReference.IsODataPrimitiveTypeKind() && !expectedPropertyTypeReference.IsNullable)
                {
                    throw new ODataException(Error.Format(SRResources.WriterValidationUtils_NonNullablePropertiesMustNotHaveNullValue, propertyName, expectedPropertyTypeReference.FullName()));
                }
                else if (expectedPropertyTypeReference.IsODataEnumTypeKind() && !expectedPropertyTypeReference.IsNullable)
                {
                    throw new ODataException(Error.Format(SRResources.WriterValidationUtils_NonNullablePropertiesMustNotHaveNullValue, propertyName, expectedPropertyTypeReference.FullName()));
                }
                else if (expectedPropertyTypeReference.IsStream())
                {
                    throw new ODataException(Error.Format(SRResources.WriterValidationUtils_StreamPropertiesMustNotHaveNullValue, propertyName));
                }
                else if (expectedPropertyTypeReference.IsODataComplexTypeKind())
                {
                    IEdmComplexTypeReference complexTypeReference = expectedPropertyTypeReference.AsComplex();
                    if (!complexTypeReference.IsNullable)
                    {
                        throw new ODataException(Error.Format(SRResources.WriterValidationUtils_NonNullablePropertiesMustNotHaveNullValue, propertyName, expectedPropertyTypeReference.FullName()));
                    }
                }
            }
        }

        /// <summary>
        /// Validates the value of the Id property on a resource.
        /// </summary>
        /// <param name="id">The id value for a resource to validate.</param>
        private static void ValidateResourceId(Uri id)
        {
            // Verify non-empty ID (entries can have no (null) ID for insert scenarios; empty IDs are not allowed)
            // TODO: it always passes. Will add more validation or remove the validation after supporting relative Uri.
            if (id != null && UriUtils.UriToString(id).Length == 0)
            {
                throw new ODataException(SRResources.WriterValidationUtils_EntriesMustHaveNonEmptyId);
            }
        }
    }
}
