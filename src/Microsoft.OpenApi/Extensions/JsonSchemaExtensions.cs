﻿using System;
using System.Collections.Generic;
using System.Text;
using Json.Schema;
using Json.Schema.OpenApi;
using Microsoft.OpenApi.Interfaces;

namespace Microsoft.OpenApi.Extensions
{
    public static class JsonSchemaExtensions
    {
        /// <summary>
        /// Gets the `discriminator` keyword if it exists.
        /// </summary>
        public static DiscriminatorKeyword? GetOpenApiDiscriminator(this JsonSchema schema)
        {
            return schema.TryGetKeyword<DiscriminatorKeyword>(DiscriminatorKeyword.Name, out var k) ? k! : null;
        }

        /// <summary>
        /// Gets the `summary` keyword if it exists.
        /// </summary>
        public static string? GetSummary(this JsonSchema schema)
        {
            return schema.TryGetKeyword<SummaryKeyword>(SummaryKeyword.Name, out var k) ? k.Summary! : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        public static bool? GetNullable(this JsonSchema schema)
        {
            return schema.TryGetKeyword<NullableKeyword>(NullableKeyword.Name, out var k) ? k.Value! : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        public static bool? GetAdditionalPropertiesAllowed(this JsonSchema schema)
        {
            return schema.TryGetKeyword<AdditionalPropertiesAllowedKeyword>(AdditionalPropertiesAllowedKeyword.Name, out var k) ? k.AdditionalPropertiesAllowed! : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        public static bool? GetOpenApiExclusiveMaximum(this JsonSchema schema)
        {
            return schema.TryGetKeyword<Draft4ExclusiveMaximumKeyword>(Draft4ExclusiveMaximumKeyword.Name, out var k) ? k.MaxValue! : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        public static bool? GetOpenApiExclusiveMinimum(this JsonSchema schema)
        {
            return schema.TryGetKeyword<Draft4ExclusiveMinimumKeyword>(Draft4ExclusiveMinimumKeyword.Name, out var k) ? k.MinValue! : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        public static IDictionary<string, IOpenApiExtension> GetExtensions(this JsonSchema schema)
        {
            return (Dictionary<string, IOpenApiExtension>)(schema.TryGetKeyword<ExtensionsKeyword>(ExtensionsKeyword.Name, out var k) ? k.Extensions! : null);
        }        
    }
}
