﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generation date: 11/6/2024 6:19:36 PM
namespace Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys
{
    /// <summary>
    /// There are no comments for Container in the schema.
    /// </summary>
    [global::Microsoft.OData.Client.OriginalNameAttribute("Container")]
    public partial class Container : global::Microsoft.OData.Client.DataServiceContext
    {
        /// <summary>
        /// Initialize a new Container object.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public Container(global::System.Uri serviceRoot) :
                this(serviceRoot, global::Microsoft.OData.Client.ODataProtocolVersion.V4)
        {
        }

        /// <summary>
        /// Initialize a new Container object.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public Container(global::System.Uri serviceRoot, global::Microsoft.OData.Client.ODataProtocolVersion protocolVersion) :
                base(serviceRoot, protocolVersion)
        {
            this.ResolveName = new global::System.Func<global::System.Type, string>(this.ResolveNameFromType);
            this.ResolveType = new global::System.Func<string, global::System.Type>(this.ResolveTypeFromName);
            this.OnContextCreated();
            this.Format.LoadServiceModel = GeneratedEdmModel.GetInstance;
            this.Format.UseJson();
        }
        partial void OnContextCreated();
        /// <summary>
        /// Since the namespace configured for this service reference
        /// in Visual Studio is different from the one indicated in the
        /// server schema, use type-mappers to map between the two.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        protected global::System.Type ResolveTypeFromName(string typeName)
        {
            global::System.Type resolvedType = this.DefaultResolveType(typeName, "Microsoft.OData.E2E.TestCommon.Common.Server.MultipleKeys", "Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys");
            if ((resolvedType != null))
            {
                return resolvedType;
            }
            return null;
        }
        /// <summary>
        /// Since the namespace configured for this service reference
        /// in Visual Studio is different from the one indicated in the
        /// server schema, use type-mappers to map between the two.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        protected string ResolveNameFromType(global::System.Type clientType)
        {
            global::Microsoft.OData.Client.OriginalNameAttribute originalNameAttribute = (global::Microsoft.OData.Client.OriginalNameAttribute)global::System.Linq.Enumerable.SingleOrDefault(global::Microsoft.OData.Client.Utility.GetCustomAttributes(clientType, typeof(global::Microsoft.OData.Client.OriginalNameAttribute), true));
            if (clientType.Namespace.Equals("Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys", global::System.StringComparison.Ordinal))
            {
                if (originalNameAttribute != null)
                {
                    return string.Concat("Microsoft.OData.E2E.TestCommon.Common.Server.MultipleKeys.", originalNameAttribute.OriginalName);
                }
                return string.Concat("Microsoft.OData.E2E.TestCommon.Common.Server.MultipleKeys.", clientType.Name);
            }
            return null;
        }
        /// <summary>
        /// There are no comments for Employees in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        [global::Microsoft.OData.Client.OriginalNameAttribute("Employees")]
        public virtual global::Microsoft.OData.Client.DataServiceQuery<EmployeeWithEnumKey> Employees
        {
            get
            {
                if ((this._Employees == null))
                {
                    this._Employees = base.CreateQuery<EmployeeWithEnumKey>("Employees");
                }
                return this._Employees;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::Microsoft.OData.Client.DataServiceQuery<EmployeeWithEnumKey> _Employees;
        /// <summary>
        /// There are no comments for Organizations in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        [global::Microsoft.OData.Client.OriginalNameAttribute("Organizations")]
        public virtual global::Microsoft.OData.Client.DataServiceQuery<Organization> Organizations
        {
            get
            {
                if ((this._Organizations == null))
                {
                    this._Organizations = base.CreateQuery<Organization>("Organizations");
                }
                return this._Organizations;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::Microsoft.OData.Client.DataServiceQuery<Organization> _Organizations;
        /// <summary>
        /// There are no comments for Employees in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual void AddToEmployees(EmployeeWithEnumKey employeeWithEnumKey)
        {
            base.AddObject("Employees", employeeWithEnumKey);
        }
        /// <summary>
        /// There are no comments for Organizations in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual void AddToOrganizations(Organization organization)
        {
            base.AddObject("Organizations", organization);
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private abstract class GeneratedEdmModel
        {
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
            private static global::Microsoft.OData.Edm.IEdmModel ParsedModel = LoadModelFromString();

            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
            private const string filePath = @"MultipleKeysEnumKeyServiceCsdl.xml";

            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
            public static global::Microsoft.OData.Edm.IEdmModel GetInstance()
            {
                return ParsedModel;
            }
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
            private static global::Microsoft.OData.Edm.IEdmModel LoadModelFromString()
            {
                global::System.Xml.XmlReader reader = CreateXmlReader();
                try
                {
                    global::System.Collections.Generic.IEnumerable<global::Microsoft.OData.Edm.Validation.EdmError> errors;
                    global::Microsoft.OData.Edm.IEdmModel edmModel;

                    if (!global::Microsoft.OData.Edm.Csdl.CsdlReader.TryParse(reader, true, out edmModel, out errors))
                    {
                        global::System.Text.StringBuilder errorMessages = new global::System.Text.StringBuilder();
                        foreach (var error in errors)
                        {
                            errorMessages.Append(error.ErrorMessage);
                            errorMessages.Append("; ");
                        }
                        throw new global::System.InvalidOperationException(errorMessages.ToString());
                    }

                    return edmModel;
                }
                finally
                {
                    ((global::System.IDisposable)(reader)).Dispose();
                }
            }
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
            private static global::System.Xml.XmlReader CreateXmlReader(string edmxToParse)
            {
                return global::System.Xml.XmlReader.Create(new global::System.IO.StringReader(edmxToParse));
            }

            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
            private static global::System.Xml.XmlReader CreateXmlReader()
            {
                try
                {
                    var assembly = global::System.Reflection.Assembly.GetExecutingAssembly();
                    var resourcePath = global::System.Linq.Enumerable.Single(assembly.GetManifestResourceNames(), str => str.EndsWith(filePath));
                    global::System.IO.Stream stream = assembly.GetManifestResourceStream(resourcePath);
                    return global::System.Xml.XmlReader.Create(new global::System.IO.StreamReader(stream));
                }
                catch (global::System.Xml.XmlException e)
                {
                    throw new global::System.Xml.XmlException("Failed to create an XmlReader from the stream. Check if the resource exists.", e);
                }
            }
        }
    }
    /// <summary>
    /// There are no comments for EmployeeWithEnumKeySingle in the schema.
    /// </summary>
    [global::Microsoft.OData.Client.OriginalNameAttribute("EmployeeWithEnumKeySingle")]
    public partial class EmployeeWithEnumKeySingle : global::Microsoft.OData.Client.DataServiceQuerySingle<EmployeeWithEnumKey>
    {
        /// <summary>
        /// Initialize a new EmployeeWithEnumKeySingle object.
        /// </summary>
        public EmployeeWithEnumKeySingle(global::Microsoft.OData.Client.DataServiceContext context, string path)
            : base(context, path) { }

        /// <summary>
        /// Initialize a new EmployeeWithEnumKeySingle object.
        /// </summary>
        public EmployeeWithEnumKeySingle(global::Microsoft.OData.Client.DataServiceContext context, string path, bool isComposable)
            : base(context, path, isComposable) { }

        /// <summary>
        /// Initialize a new EmployeeWithEnumKeySingle object.
        /// </summary>
        public EmployeeWithEnumKeySingle(global::Microsoft.OData.Client.DataServiceQuerySingle<EmployeeWithEnumKey> query)
            : base(query) { }

        /// <summary>
        /// There are no comments for Organization in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        [global::Microsoft.OData.Client.OriginalNameAttribute("Organization")]
        public virtual global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.OrganizationSingle Organization
        {
            get
            {
                if (!this.IsComposable)
                {
                    throw new global::System.NotSupportedException("The previous function is not composable.");
                }
                if ((this._Organization == null))
                {
                    this._Organization = new global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.OrganizationSingle(this.Context, GetPath("Organization"));
                }
                return this._Organization;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.OrganizationSingle _Organization;
    }
    /// <summary>
    /// There are no comments for EmployeeWithEnumKey in the schema.
    /// </summary>
    /// <KeyProperties>
    /// EmployeeNumber
    /// EmployeeType
    /// </KeyProperties>
    [global::Microsoft.OData.Client.Key("EmployeeNumber", "EmployeeType")]
    [global::Microsoft.OData.Client.EntitySet("Employees")]
    [global::Microsoft.OData.Client.OriginalNameAttribute("EmployeeWithEnumKey")]
    public partial class EmployeeWithEnumKey : global::Microsoft.OData.Client.BaseEntityType, global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Create a new EmployeeWithEnumKey object.
        /// </summary>
        /// <param name="employeeNumber">Initial value of EmployeeNumber.</param>
        /// <param name="employeeType">Initial value of EmployeeType.</param>
        /// <param name="name">Initial value of Name.</param>
        /// <param name="salary">Initial value of Salary.</param>
        /// <param name="organizationId">Initial value of OrganizationId.</param>
        /// <param name="organization">Initial value of Organization.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public static EmployeeWithEnumKey CreateEmployeeWithEnumKey(int employeeNumber,
                    global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.EmployeeType employeeType,
                    string name,
                    decimal salary,
                    int organizationId,
                    global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.Organization organization)
        {
            EmployeeWithEnumKey employeeWithEnumKey = new EmployeeWithEnumKey();
            employeeWithEnumKey.EmployeeNumber = employeeNumber;
            employeeWithEnumKey.EmployeeType = employeeType;
            employeeWithEnumKey.Name = name;
            employeeWithEnumKey.Salary = salary;
            employeeWithEnumKey.OrganizationId = organizationId;
            if ((organization == null))
            {
                throw new global::System.ArgumentNullException("organization");
            }
            employeeWithEnumKey.Organization = organization;
            return employeeWithEnumKey;
        }
        /// <summary>
        /// There are no comments for Property EmployeeNumber in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        [global::Microsoft.OData.Client.OriginalNameAttribute("EmployeeNumber")]
        [global::System.ComponentModel.DataAnnotations.RequiredAttribute(ErrorMessage = "EmployeeNumber is required.")]
        public virtual int EmployeeNumber
        {
            get
            {
                return this._EmployeeNumber;
            }
            set
            {
                this.OnEmployeeNumberChanging(value);
                this._EmployeeNumber = value;
                this.OnEmployeeNumberChanged();
                this.OnPropertyChanged("EmployeeNumber");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private int _EmployeeNumber;
        partial void OnEmployeeNumberChanging(int value);
        partial void OnEmployeeNumberChanged();
        /// <summary>
        /// There are no comments for Property EmployeeType in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        [global::Microsoft.OData.Client.OriginalNameAttribute("EmployeeType")]
        [global::System.ComponentModel.DataAnnotations.RequiredAttribute(ErrorMessage = "EmployeeType is required.")]
        public virtual global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.EmployeeType EmployeeType
        {
            get
            {
                return this._EmployeeType;
            }
            set
            {
                this.OnEmployeeTypeChanging(value);
                this._EmployeeType = value;
                this.OnEmployeeTypeChanged();
                this.OnPropertyChanged("EmployeeType");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.EmployeeType _EmployeeType;
        partial void OnEmployeeTypeChanging(global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.EmployeeType value);
        partial void OnEmployeeTypeChanged();
        /// <summary>
        /// There are no comments for Property Name in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        [global::Microsoft.OData.Client.OriginalNameAttribute("Name")]
        [global::System.ComponentModel.DataAnnotations.RequiredAttribute(ErrorMessage = "Name is required.")]
        public virtual string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this.OnNameChanging(value);
                this._Name = value;
                this.OnNameChanged();
                this.OnPropertyChanged("Name");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _Name;
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        /// <summary>
        /// There are no comments for Property Salary in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        [global::Microsoft.OData.Client.OriginalNameAttribute("Salary")]
        [global::System.ComponentModel.DataAnnotations.RequiredAttribute(ErrorMessage = "Salary is required.")]
        public virtual decimal Salary
        {
            get
            {
                return this._Salary;
            }
            set
            {
                this.OnSalaryChanging(value);
                this._Salary = value;
                this.OnSalaryChanged();
                this.OnPropertyChanged("Salary");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private decimal _Salary;
        partial void OnSalaryChanging(decimal value);
        partial void OnSalaryChanged();
        /// <summary>
        /// There are no comments for Property OrganizationId in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        [global::Microsoft.OData.Client.OriginalNameAttribute("OrganizationId")]
        [global::System.ComponentModel.DataAnnotations.RequiredAttribute(ErrorMessage = "OrganizationId is required.")]
        public virtual int OrganizationId
        {
            get
            {
                return this._OrganizationId;
            }
            set
            {
                this.OnOrganizationIdChanging(value);
                this._OrganizationId = value;
                this.OnOrganizationIdChanged();
                this.OnPropertyChanged("OrganizationId");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private int _OrganizationId;
        partial void OnOrganizationIdChanging(int value);
        partial void OnOrganizationIdChanged();
        /// <summary>
        /// There are no comments for Property Organization in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        [global::Microsoft.OData.Client.OriginalNameAttribute("Organization")]
        [global::System.ComponentModel.DataAnnotations.RequiredAttribute(ErrorMessage = "Organization is required.")]
        public virtual global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.Organization Organization
        {
            get
            {
                return this._Organization;
            }
            set
            {
                this.OnOrganizationChanging(value);
                this._Organization = value;
                this.OnOrganizationChanged();
                this.OnPropertyChanged("Organization");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.Organization _Organization;
        partial void OnOrganizationChanging(global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.Organization value);
        partial void OnOrganizationChanged();
        /// <summary>
        /// This event is raised when the value of the property is changed
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// The value of the property is changed
        /// </summary>
        /// <param name="property">property name</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
    /// <summary>
    /// There are no comments for OrganizationSingle in the schema.
    /// </summary>
    [global::Microsoft.OData.Client.OriginalNameAttribute("OrganizationSingle")]
    public partial class OrganizationSingle : global::Microsoft.OData.Client.DataServiceQuerySingle<Organization>
    {
        /// <summary>
        /// Initialize a new OrganizationSingle object.
        /// </summary>
        public OrganizationSingle(global::Microsoft.OData.Client.DataServiceContext context, string path)
            : base(context, path) { }

        /// <summary>
        /// Initialize a new OrganizationSingle object.
        /// </summary>
        public OrganizationSingle(global::Microsoft.OData.Client.DataServiceContext context, string path, bool isComposable)
            : base(context, path, isComposable) { }

        /// <summary>
        /// Initialize a new OrganizationSingle object.
        /// </summary>
        public OrganizationSingle(global::Microsoft.OData.Client.DataServiceQuerySingle<Organization> query)
            : base(query) { }

        /// <summary>
        /// There are no comments for Employees in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        [global::Microsoft.OData.Client.OriginalNameAttribute("Employees")]
        public virtual global::Microsoft.OData.Client.DataServiceQuery<global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.EmployeeWithEnumKey> Employees
        {
            get
            {
                if (!this.IsComposable)
                {
                    throw new global::System.NotSupportedException("The previous function is not composable.");
                }
                if ((this._Employees == null))
                {
                    this._Employees = Context.CreateQuery<global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.EmployeeWithEnumKey>(GetPath("Employees"));
                }
                return this._Employees;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::Microsoft.OData.Client.DataServiceQuery<global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.EmployeeWithEnumKey> _Employees;
    }
    /// <summary>
    /// There are no comments for Organization in the schema.
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [global::Microsoft.OData.Client.Key("Id")]
    [global::Microsoft.OData.Client.EntitySet("Organizations")]
    [global::Microsoft.OData.Client.OriginalNameAttribute("Organization")]
    public partial class Organization : global::Microsoft.OData.Client.BaseEntityType, global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Create a new Organization object.
        /// </summary>
        /// <param name="ID">Initial value of Id.</param>
        /// <param name="name">Initial value of Name.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public static Organization CreateOrganization(int ID, string name)
        {
            Organization organization = new Organization();
            organization.Id = ID;
            organization.Name = name;
            return organization;
        }
        /// <summary>
        /// There are no comments for Property Id in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        [global::Microsoft.OData.Client.OriginalNameAttribute("Id")]
        [global::System.ComponentModel.DataAnnotations.RequiredAttribute(ErrorMessage = "Id is required.")]
        public virtual int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private int _Id;
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        /// <summary>
        /// There are no comments for Property Name in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        [global::Microsoft.OData.Client.OriginalNameAttribute("Name")]
        [global::System.ComponentModel.DataAnnotations.RequiredAttribute(ErrorMessage = "Name is required.")]
        public virtual string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this.OnNameChanging(value);
                this._Name = value;
                this.OnNameChanged();
                this.OnPropertyChanged("Name");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _Name;
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        /// <summary>
        /// There are no comments for Property Employees in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        [global::Microsoft.OData.Client.OriginalNameAttribute("Employees")]
        public virtual global::Microsoft.OData.Client.DataServiceCollection<global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.EmployeeWithEnumKey> Employees
        {
            get
            {
                return this._Employees;
            }
            set
            {
                this.OnEmployeesChanging(value);
                this._Employees = value;
                this.OnEmployeesChanged();
                this.OnPropertyChanged("Employees");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::Microsoft.OData.Client.DataServiceCollection<global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.EmployeeWithEnumKey> _Employees = new global::Microsoft.OData.Client.DataServiceCollection<global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.EmployeeWithEnumKey>(null, global::Microsoft.OData.Client.TrackingMode.None);
        partial void OnEmployeesChanging(global::Microsoft.OData.Client.DataServiceCollection<global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.EmployeeWithEnumKey> value);
        partial void OnEmployeesChanged();
        /// <summary>
        /// This event is raised when the value of the property is changed
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// The value of the property is changed
        /// </summary>
        /// <param name="property">property name</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
    /// <summary>
    /// There are no comments for EmployeeType in the schema.
    /// </summary>
    [global::Microsoft.OData.Client.OriginalNameAttribute("EmployeeType")]
    public enum EmployeeType
    {
        [global::Microsoft.OData.Client.OriginalNameAttribute("FullTime")]
        FullTime = 0,
        [global::Microsoft.OData.Client.OriginalNameAttribute("PartTime")]
        PartTime = 1,
        [global::Microsoft.OData.Client.OriginalNameAttribute("Contractor")]
        Contractor = 2
    }
    /// <summary>
    /// Class containing all extension methods
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Get an entity of type global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.EmployeeWithEnumKey as global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.EmployeeWithEnumKeySingle specified by key from an entity set
        /// </summary>
        /// <param name="_source">source entity set</param>
        /// <param name="_keys">dictionary with the names and values of keys</param>
        public static global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.EmployeeWithEnumKeySingle ByKey(this global::Microsoft.OData.Client.DataServiceQuery<global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.EmployeeWithEnumKey> _source, global::System.Collections.Generic.IDictionary<string, object> _keys)
        {
            return new global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.EmployeeWithEnumKeySingle(_source.Context, _source.GetKeyPath(global::Microsoft.OData.Client.Serializer.GetKeyString(_source.Context, _keys)));
        }
        /// <summary>
        /// Get an entity of type global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.EmployeeWithEnumKey as global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.EmployeeWithEnumKeySingle specified by key from an entity set
        /// </summary>
        /// <param name="_source">source entity set</param>
        /// <param name="employeeNumber">The value of employeeNumber</param>
        /// <param name="employeeType">The value of employeeType</param>
        public static global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.EmployeeWithEnumKeySingle ByKey(this global::Microsoft.OData.Client.DataServiceQuery<global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.EmployeeWithEnumKey> _source,
            int employeeNumber,
            global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.EmployeeType employeeType)
        {
            global::System.Collections.Generic.IDictionary<string, object> _keys = new global::System.Collections.Generic.Dictionary<string, object>
            {
                { "EmployeeNumber", employeeNumber },
                { "EmployeeType", employeeType }
            };
            return new global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.EmployeeWithEnumKeySingle(_source.Context, _source.GetKeyPath(global::Microsoft.OData.Client.Serializer.GetKeyString(_source.Context, _keys)));
        }
        /// <summary>
        /// Get an entity of type global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.Organization as global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.OrganizationSingle specified by key from an entity set
        /// </summary>
        /// <param name="_source">source entity set</param>
        /// <param name="_keys">dictionary with the names and values of keys</param>
        public static global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.OrganizationSingle ByKey(this global::Microsoft.OData.Client.DataServiceQuery<global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.Organization> _source, global::System.Collections.Generic.IDictionary<string, object> _keys)
        {
            return new global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.OrganizationSingle(_source.Context, _source.GetKeyPath(global::Microsoft.OData.Client.Serializer.GetKeyString(_source.Context, _keys)));
        }
        /// <summary>
        /// Get an entity of type global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.Organization as global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.OrganizationSingle specified by key from an entity set
        /// </summary>
        /// <param name="_source">source entity set</param>
        /// <param name="id">The value of id</param>
        public static global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.OrganizationSingle ByKey(this global::Microsoft.OData.Client.DataServiceQuery<global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.Organization> _source,
            int id)
        {
            global::System.Collections.Generic.IDictionary<string, object> _keys = new global::System.Collections.Generic.Dictionary<string, object>
            {
                { "Id", id }
            };
            return new global::Microsoft.OData.E2E.TestCommon.Common.Client.MultipleKeys.OrganizationSingle(_source.Context, _source.GetKeyPath(global::Microsoft.OData.Client.Serializer.GetKeyString(_source.Context, _keys)));
        }
    }
}
