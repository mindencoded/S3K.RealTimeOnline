﻿using System;
using System.IO;
using System.Security.Cryptography;

namespace Northwind.WebRole.Security
{
    public class RsaStore
    {
        public static void Add(string keyContainerName)
        {
            RSACryptoServiceProvider rsa = GetServiceProvider(keyContainerName);
            if (rsa != null)
            {
                string xmlStringPrivateKey = rsa.ToXmlString(true);
                string xmlStringPublicKey = rsa.ToXmlString(false);
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string rsaPrivateKeyPath = Path.Combine(baseDirectory, keyContainerName + "-rsa-private-key.xml");
                string rsaPublicKeyPath = Path.Combine(baseDirectory, keyContainerName + "-rsa-public-key.xml");
                File.WriteAllText(rsaPrivateKeyPath, xmlStringPrivateKey);
                File.WriteAllText(rsaPublicKeyPath, xmlStringPublicKey);
            }
        }

        public static RSACryptoServiceProvider GetServiceProvider(string keyContainerName, bool persistKeyInCsp = true)
        {
            CspParameters parameters = new CspParameters
            {
                KeyContainerName = keyContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            return new RSACryptoServiceProvider(2048, parameters) {PersistKeyInCsp = persistKeyInCsp};
        }

        public static RSACryptoServiceProvider Load(string keyContainerName)
        {
            RSACryptoServiceProvider rsa = GetServiceProvider(keyContainerName);
            if (rsa == null)
            {
                throw new Exception("RSACryptoServiceProvider not found.");
            }

            rsa.Clear();
            string rsaPrivateKeyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                keyContainerName.ToLower().Replace("-", "_") + "-rsa-private-key.xml");
            string xmlPrivateKey = File.ReadAllText(rsaPrivateKeyPath);
            rsa.FromXmlString(xmlPrivateKey);
            return rsa;
        }

        public static void Remove(string keyContainerName)
        {
            RSACryptoServiceProvider rsa = GetServiceProvider(keyContainerName);
            if (rsa == null)
            {
                throw new Exception("RSACryptoServiceProvider not found.");
            }

            rsa.Clear();
            rsa.PersistKeyInCsp = false;
        }
    }
}