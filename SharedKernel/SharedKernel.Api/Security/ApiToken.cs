using System;
using Newtonsoft.Json;
using JWT;
using SharedKernel.Domain.Dtos;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Http;

namespace SharedKernel.Api.Security
{
    public static class ApiToken
    {
        public static string Secret { get; set; }

        public static void GerarSecret()
        {
            //var secret = Guid.NewGuid().ToString("N").Substring(0, 20);
            //Secret = secret;

            //TODO: Secrect est� fixo aqui, mas devemos colocar num arquivo de configura��o ou Vault 
            //TODO: - N�o devemos gerar como antes, sen�o n�o poderemos subir a api em varios servidores

            Secret = "1c57ca00ddb84675ac87";
        }

        public static string GerarTokenString(this Token token)
        {
            if (string.IsNullOrWhiteSpace(Secret))
                throw new Exception("The Secrect Key was not generated!");

            var algorithm = new HMACSHA256Algorithm();
            var serializer = new JsonNetSerializer();
            var urlEncoder = new JwtBase64UrlEncoder();
            var encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var tokenString = encoder.Encode(token, Secret);

            return tokenString;
        }

        public static string RecuperarTokenString(this HttpContext context)
        {
            return context.Request.Headers["Token"];
        }

        public static Token RecuperarToken(this HttpContext context)
        {
            try
            {
                var tokenString = context.RecuperarTokenString();

                var serializer = new JsonNetSerializer();
                var provider = new UtcDateTimeProvider();
                var validator = new JwtValidator(serializer, provider);
                var urlEncoder = new JwtBase64UrlEncoder();
                var decoder = new JwtDecoder(serializer, validator, urlEncoder);
                
                var json = decoder.Decode(tokenString, Secret, verify: true);

                return JsonConvert.DeserializeObject<Token>(json);
            }
            catch (SignatureVerificationException)
            {
                Console.Out.WriteLine("Invalid Token!");
            }
            return null;
        }
    }
}