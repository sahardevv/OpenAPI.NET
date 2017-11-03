﻿// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Writers;

namespace Microsoft.OpenApi.Models
{
    /// <summary>
    /// ExternalDocs object.
    /// </summary>
    public class OpenApiEncoding : OpenApiElement, IOpenApiExtension
    {
        /// <summary>
        /// The Content-Type for encoding a specific property.
        /// The value can be a specific media type (e.g. application/json),
        /// a wildcard media type (e.g. image/*), or a comma-separated list of the two types.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// A map allowing additional information to be provided as headers.
        /// </summary>
        public IDictionary<string, OpenApiHeader> Headers { get; set; }

        /// <summary>
        /// Describes how a specific property value will be serialized depending on its type. 
        /// </summary>
        public ParameterStyle? Style { get; set; }

        /// <summary>
        /// When this is true, property values of type array or object generate separate parameters
        /// for each value of the array, or key-value-pair of the map. For other types of properties
        /// this property has no effect. When style is form, the default value is true.
        /// For all other styles, the default value is false.
        /// This property SHALL be ignored if the request body media type is not application/x-www-form-urlencoded.
        /// </summary>
        public bool? Explode { get; set; }

        /// <summary>
        /// Determines whether the parameter value SHOULD allow reserved characters,
        /// as defined by RFC3986 :/?#[]@!$&'()*+,;= to be included without percent-encoding.
        /// The default value is false. This property SHALL be ignored
        /// if the request body media type is not application/x-www-form-urlencoded.
        /// </summary>
        public bool? AllowReserved { get; set; }

        /// <summary>
        /// This object MAY be extended with Specification Extensions.
        /// </summary>
        public IDictionary<string, IOpenApiAny> Extensions { get; set; }

        /// <summary>
        /// Serialize <see cref="OpenApiExternalDocs"/> to Open Api v3.0.
        /// </summary>
        internal override void WriteAsV3(IOpenApiWriter writer)
        {
            if (writer == null)
            {
                throw Error.ArgumentNull("writer");
            }

            writer.WriteStartObject();

            writer.WriteStringProperty(OpenApiConstants.ContentType, ContentType);

            writer.WriteMap(OpenApiConstants.Headers, Headers, (w, h) => h.WriteAsV3(w));

            writer.WriteStringProperty(OpenApiConstants.Style, Style?.ToString());

            if (Explode != null)
            {
                writer.WriteBoolProperty(OpenApiConstants.Explode, Explode.Value, false);
            }

            if (AllowReserved != null)
            {
                writer.WriteBoolProperty(OpenApiConstants.AllowReserved, AllowReserved.Value, false);
            }

            writer.WriteExtensions(Extensions);

            writer.WriteEndObject();
        }

        /// <summary>
        /// Serialize <see cref="OpenApiExternalDocs"/> to Open Api v2.0.
        /// </summary>
        internal override void WriteAsV2(IOpenApiWriter writer)
        {
            // nothing here
        }
    }
}