using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azeroth.WebApi
{
    public class JwtHelper
    {
        static string key = "helloworld1111111111111222222222222222";
        public static string Encode(object target)
        {
            JWT.Algorithms.HMACSHA256Algorithm algorithm = new JWT.Algorithms.HMACSHA256Algorithm();
            JWT.Serializers.JsonNetSerializer serializer = new JWT.Serializers.JsonNetSerializer();
            JWT.JwtBase64UrlEncoder urlEncoder = new JWT.JwtBase64UrlEncoder();
            JWT.JwtEncoder encoder = new JWT.JwtEncoder(algorithm, serializer, urlEncoder);
            var token = encoder.Encode(target, key);
            return token;
        }

        public static T Decode<T>(string token)
        {
            JWT.Serializers.JsonNetSerializer serializer = new JWT.Serializers.JsonNetSerializer();
            JWT.UtcDateTimeProvider provider = new JWT.UtcDateTimeProvider();
            JWT.JwtValidator validator = new JWT.JwtValidator(serializer, provider);
            JWT.JwtBase64UrlEncoder urlEncoder = new JWT.JwtBase64UrlEncoder();
            JWT.JwtDecoder decoder = new JWT.JwtDecoder(serializer, validator, urlEncoder);
            T target = decoder.DecodeToObject<T>(token, key, true);
            return target;
        }
    }
}