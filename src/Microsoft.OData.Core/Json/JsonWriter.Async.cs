﻿//---------------------------------------------------------------------
// <copyright file="JsonWriter.Async.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

namespace Microsoft.OData.Json
{
    #region Namespaces
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.OData.Buffers;
    using Microsoft.OData.Edm;
    #endregion Namespaces

    internal sealed partial class JsonWriter
    {
        /// <inheritdoc/>
        public Task StartPaddingFunctionScopeAsync()
        {
            Debug.Assert(this.scopes.Count == 0, "Padding scope can only be the outer most scope.");
            return this.StartScopeAsync(ScopeType.Padding);
        }

        /// <inheritdoc/>
        public async Task EndPaddingFunctionScopeAsync()
        {
            Debug.Assert(this.scopes.Count > 0, "No scope to end.");

            await this.writer.WriteLineAsync().ConfigureAwait(false);
            await this.writer.DecreaseIndentationAsync().ConfigureAwait(false);
            Scope scope = this.scopes.Pop();

            Debug.Assert(scope.Type == ScopeType.Padding, "Ending scope does not match.");

            await this.writer.WriteAsync(scope.EndString).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public Task StartObjectScopeAsync()
        {
            return this.StartScopeAsync(ScopeType.Object);
        }

        /// <inheritdoc/>
        public async Task EndObjectScopeAsync()
        {
            Debug.Assert(this.scopes.Count > 0, "No scope to end.");

            await this.writer.WriteLineAsync().ConfigureAwait(false);
            await this.writer.DecreaseIndentationAsync().ConfigureAwait(false);
            Scope scope = this.scopes.Pop();

            Debug.Assert(scope.Type == ScopeType.Object, "Ending scope does not match.");

            await this.writer.WriteAsync(scope.EndString).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public Task StartArrayScopeAsync()
        {
            return this.StartScopeAsync(ScopeType.Array);
        }

        /// <inheritdoc/>
        public async Task EndArrayScopeAsync()
        {
            Debug.Assert(this.scopes.Count > 0, "No scope to end.");

            await this.writer.WriteLineAsync().ConfigureAwait(false);
            await this.writer.DecreaseIndentationAsync().ConfigureAwait(false);
            Scope scope = this.scopes.Pop();

            Debug.Assert(scope.Type == ScopeType.Array, "Ending scope does not match.");

            await this.writer.WriteAsync(scope.EndString).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task WriteNameAsync(string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(name), "The name must be specified.");
            Debug.Assert(this.scopes.Count > 0, "There must be an active scope for name to be written.");
            Debug.Assert(this.scopes.Peek().Type == ScopeType.Object, "The active scope must be an object scope for name to be written.");

            Scope currentScope = this.scopes.Peek();
            if (currentScope.ObjectCount != 0)
            {
                await this.writer.WriteAsync(JsonConstants.ObjectMemberSeparator).ConfigureAwait(false);
            }

            currentScope.ObjectCount++;

            await this.writer.WriteEscapedJsonStringAsync(name, this.stringEscapeOption, this.wrappedBuffer, this.ArrayPool)
                .ConfigureAwait(false);
            await this.writer.WriteAsync(JsonConstants.NameValueSeparator).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public Task WritePaddingFunctionNameAsync(string functionName)
        {
            return this.writer.WriteAsync(functionName);
        }

        /// <inheritdoc/>
        public async Task WriteValueAsync(bool value)
        {
            await this.WriteValueSeparatorAsync().ConfigureAwait(false);
            await this.writer.WriteValueAsync(value).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task WriteValueAsync(int value)
        {
            await this.WriteValueSeparatorAsync().ConfigureAwait(false);
            await this.writer.WriteValueAsync(value).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task WriteValueAsync(float value)
        {
            await this.WriteValueSeparatorAsync().ConfigureAwait(false);
            await this.writer.WriteValueAsync(value).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task WriteValueAsync(short value)
        {
            await this.WriteValueSeparatorAsync().ConfigureAwait(false);
            await this.writer.WriteValueAsync(value).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task WriteValueAsync(long value)
        {
            await this.WriteValueSeparatorAsync().ConfigureAwait(false);

            // if it is IEEE754Compatible, write numbers with quotes; otherwise, write numbers directly.
            if (isIeee754Compatible)
            {
                await this.writer.WriteValueAsync(value.ToString(CultureInfo.InvariantCulture),
                    this.stringEscapeOption, this.wrappedBuffer, this.ArrayPool).ConfigureAwait(false);
            }
            else
            {
                await this.writer.WriteValueAsync(value).ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task WriteValueAsync(double value)
        {
            await this.WriteValueSeparatorAsync().ConfigureAwait(false);
            await this.writer.WriteValueAsync(value).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task WriteValueAsync(Guid value)
        {
            await this.WriteValueSeparatorAsync().ConfigureAwait(false);
            await this.writer.WriteValueAsync(value).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task WriteValueAsync(decimal value)
        {
            await this.WriteValueSeparatorAsync().ConfigureAwait(false);

            // if it is not IEEE754Compatible, write numbers directly without quotes;
            if (isIeee754Compatible)
            {
                await this.writer.WriteValueAsync(value.ToString(CultureInfo.InvariantCulture),
                    this.stringEscapeOption, this.wrappedBuffer, this.ArrayPool).ConfigureAwait(false);
            }
            else
            {
                await this.writer.WriteValueAsync(value).ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task WriteValueAsync(DateTimeOffset value)
        {
            await this.WriteValueSeparatorAsync().ConfigureAwait(false);
            await this.writer.WriteValueAsync(value, ODataJsonDateTimeFormat.ISO8601DateTime)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task WriteValueAsync(TimeSpan value)
        {
            await this.WriteValueSeparatorAsync().ConfigureAwait(false);
            await this.writer.WriteValueAsync(value).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task WriteValueAsync(TimeOfDay value)
        {
            await this.WriteValueSeparatorAsync().ConfigureAwait(false);
            await this.writer.WriteValueAsync(value).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task WriteValueAsync(Date value)
        {
            await this.WriteValueSeparatorAsync().ConfigureAwait(false);
            await this.writer.WriteValueAsync(value).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task WriteValueAsync(byte value)
        {
            await this.WriteValueSeparatorAsync().ConfigureAwait(false);
            await this.writer.WriteValueAsync(value).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task WriteValueAsync(sbyte value)
        {
            await this.WriteValueSeparatorAsync().ConfigureAwait(false);
            await this.writer.WriteValueAsync(value).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task WriteValueAsync(string value)
        {
            await this.WriteValueSeparatorAsync().ConfigureAwait(false);
            await this.writer.WriteValueAsync(value, this.stringEscapeOption, this.wrappedBuffer, this.ArrayPool)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task WriteValueAsync(byte[] value)
        {
            await this.WriteValueSeparatorAsync().ConfigureAwait(false);
            await this.writer.WriteValueAsync(value, this.wrappedBuffer, this.ArrayPool).ConfigureAwait(false);
        }

        public Task WriteValueAsync(JsonElement value)
        {
            switch (value.ValueKind)
            {
                case JsonValueKind.Null:
                    return this.WriteValueAsync((string)null);
                case JsonValueKind.False:
                    return this.WriteValueAsync(false);
                case JsonValueKind.True:
                    return this.WriteValueAsync(true);
                case JsonValueKind.String:
                    return this.WriteValueAsync(value.GetString());
                case JsonValueKind.Array:
                    return this.WriteJsonElementArrayAsync(value);
                case JsonValueKind.Object:
                    return this.WriteJsonElementObjectAsync(value);
                case JsonValueKind.Number:
                    return this.WriteJsonElementNumberAsync(value);
                default:
                    // we've exhausted all known JSON types, if we get
                    // to this point, then it's undefined behavior
                    Debug.Fail($"Unexpected JSON ValueKind {value.ValueKind}");
                    return Task.CompletedTask;
            }
        }

        private async Task WriteJsonElementArrayAsync(JsonElement value)
        {
            Debug.Assert(value.ValueKind == JsonValueKind.Array);

            await this.StartArrayScopeAsync().ConfigureAwait(false);
            foreach (JsonElement item in value.EnumerateArray())
            {
                await this.WriteValueAsync(item).ConfigureAwait(false);
            }

            await this.EndArrayScopeAsync().ConfigureAwait(false);
        }

        private async Task WriteJsonElementObjectAsync(JsonElement value)
        {
            Debug.Assert(value.ValueKind == JsonValueKind.Object);

            await this.StartObjectScopeAsync().ConfigureAwait(false);
            foreach (JsonProperty property in value.EnumerateObject())
            {
                await this.WriteNameAsync(property.Name).ConfigureAwait(false);
                await this.WriteValueAsync(property.Value).ConfigureAwait(false);
            }

            await this.EndObjectScopeAsync().ConfigureAwait(false);
        }

        private Task WriteJsonElementNumberAsync(JsonElement value)
        {
            Debug.Assert(value.ValueKind == JsonValueKind.Number);

            if (value.TryGetByte(out byte byteValue))
            {
                return this.WriteValueAsync(byteValue);
            }

            if (value.TryGetDecimal(out decimal decimalValue))
            {
                return this.WriteValueAsync(decimalValue);
            }

            if (value.TryGetDouble(out double doubleValue))
            {
                return WriteValueAsync(doubleValue);
            }

            if (value.TryGetInt16(out short shortValue))
            {
                return this.WriteValueAsync(shortValue);
            }

            if (value.TryGetInt32(out int intValue))
            {
                return this.WriteValueAsync(intValue);
            }

            if (value.TryGetInt64(out long longValue))
            {
                return this.WriteValueAsync(longValue);
            }

            if (value.TryGetSByte(out sbyte sbyteValue))
            {
                return this.WriteValueAsync(sbyteValue);
            }

            if (value.TryGetSingle(out float floatValue))
            {
                return this.WriteValueAsync(floatValue);
            }

            if (value.TryGetUInt16(out ushort ushortValue))
            {
                return this.WriteValueAsync(ushortValue);
            }

            if (value.TryGetUInt32(out uint uintValue))
            {
                return this.WriteValueAsync(uintValue);
            }

            if (value.TryGetUInt64(out ulong ulongValue))
            {
                return this.WriteValueAsync((decimal)ulongValue);
            }

            Debug.Fail($"Exhausted all known JSON number types and did not find match.");
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public async Task WriteRawValueAsync(string rawValue)
        {
            await this.WriteValueSeparatorAsync().ConfigureAwait(false);
            await this.writer.WriteAsync(rawValue).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public Task FlushAsync()
        {
            return this.writer.FlushAsync();
        }

        public async ValueTask DisposeAsync()
        {
            if (this.ArrayPool != null && this.wrappedBuffer.Value != null)
            {
                BufferUtils.ReturnToBuffer(this.ArrayPool, this.wrappedBuffer.Value);
                this.ArrayPool = null;
                this.wrappedBuffer.Value = null;
            }

            if (this.binaryValueStream != null)
            {
                try
                {
                    await this.binaryValueStream.DisposeAsync().ConfigureAwait(false);
                }
                finally
                {
                    this.binaryValueStream = null;
                }
            }
        }

        /// <inheritdoc/>
        public async Task<Stream> StartStreamValueScopeAsync()
        {
            await this.WriteValueSeparatorAsync().ConfigureAwait(false);
            await this.writer.WriteAsync(JsonConstants.QuoteCharacter).ConfigureAwait(false);
            await this.writer.FlushAsync().ConfigureAwait(false);
            // IMPORTANT: The Dispose method of the returned stream does the following:
            // - Writes trailing bytes to the writer synchronously
            // - Flushes the buffer of the writer synchronously
            // ODL supports net45 and netstandard1.1 (in addition to .netstandard2.0)
            // This makes it complicated to implement IAsyncDisposable in ODataBinaryStreamWriter
            // TODO: Can the returned stream be safely used asynchronously?
            this.binaryValueStream = new ODataBinaryStreamWriter(writer, this.wrappedBuffer, this.ArrayPool);
            return this.binaryValueStream;
        }

        /// <inheritdoc/>
        public async Task EndStreamValueScopeAsync()
        {
            await this.binaryValueStream.FlushAsync().ConfigureAwait(false);
            await this.binaryValueStream.DisposeAsync().ConfigureAwait(false);

            this.binaryValueStream = null;
            await this.writer.FlushAsync().ConfigureAwait(false);
            await this.writer.WriteAsync(JsonConstants.QuoteCharacter).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<TextWriter> StartTextWriterValueScopeAsync(string contentType)
        {
            await this.WriteValueSeparatorAsync().ConfigureAwait(false);
            this.currentContentType = contentType;
            if (!IsWritingJson)
            {
                await this.writer.WriteAsync(JsonConstants.QuoteCharacter)
                    .ConfigureAwait(false);
                await this.writer.FlushAsync().ConfigureAwait(false);
                return new ODataJsonTextWriter(writer, this.wrappedBuffer, this.ArrayPool);
            }

            await this.writer.FlushAsync().ConfigureAwait(false);

            return this.writer;
        }

        /// <inheritdoc/>
        public Task EndTextWriterValueScopeAsync()
        {
            if (!IsWritingJson)
            {
                return this.writer.WriteAsync(JsonConstants.QuoteCharacter);
            }

            return TaskUtils.CompletedTask;
        }

        /// <summary>
        /// Asynchronously writes a separator of a value if it's needed for the next value to be written.
        /// </summary>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        private Task WriteValueSeparatorAsync()
        {
            if (this.scopes.Count == 0)
            {
                return TaskUtils.CompletedTask;
            }

            Scope currentScope = this.scopes.Peek();
            if (currentScope.Type == ScopeType.Array)
            {
                currentScope.ObjectCount++;
                if (currentScope.ObjectCount > 1)
                {
                    return this.writer.WriteAsync(JsonConstants.ArrayElementSeparator);
                }
            }

            return TaskUtils.CompletedTask;
        }

        /// <summary>
        /// Asynchronously starts the scope given the scope type.
        /// </summary>
        /// <param name="type">The scope type to start.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        private async Task StartScopeAsync(ScopeType type)
        {
            if (this.scopes.Count != 0 && this.scopes.Peek().Type != ScopeType.Padding)
            {
                Scope currentScope = this.scopes.Peek();
                if ((currentScope.Type == ScopeType.Array) &&
                    (currentScope.ObjectCount != 0))
                {
                    await this.writer.WriteAsync(JsonConstants.ArrayElementSeparator)
                        .ConfigureAwait(false);
                }

                currentScope.ObjectCount++;
            }

            Scope scope = new Scope(type);
            this.scopes.Push(scope);

            await this.writer.WriteAsync(scope.StartString).ConfigureAwait(false);
            await this.writer.IncreaseIndentationAsync().ConfigureAwait(false);
            await this.writer.WriteLineAsync().ConfigureAwait(false);
        }
    }
}
