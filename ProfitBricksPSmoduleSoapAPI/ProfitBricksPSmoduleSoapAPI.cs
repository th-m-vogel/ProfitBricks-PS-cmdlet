//Copyright 2013 Thomas Vogel
//
//Licensed under the Apache License, Version 2.0 (the "License"); you may 
//not use this file except in compliance with the License. You may obtain 
//a copy of the License at
//
//http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software distributed 
//under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
//CONDITIONS OF ANY KIND, either express or implied. See the License for the specific 
//language governing permissions and limitations under the License.



using ProfitBricksPSmoduleSoapAPI.WsProfitBricksApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Management.Automation;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Text;

namespace ProfitBricksPSmoduleSoapAPI
{
    #region PBapiPSCmdlet : PSCmdlet
    public class PBapiPSCmdlet : PSCmdlet
    {
        protected void WriteObjects(IEnumerable objects)
        {
            if (objects == null)
            {
                this.WriteObject(null);
            }
            else
            {
                foreach (var obj in objects)
                {
                    WriteObject(obj);
                }
            }
        }

        protected override void BeginProcessing()
        {
            if (String.IsNullOrEmpty(PBApi.Service.ClientCredentials.UserName.UserName))
            {
                throw new System.ApplicationException("soap API is not initialized or no credentials given. please use Open-PBApiService first!");
            }
        }
    }
    #endregion

    #region PBapiChecks
    public class PBapiChecks
    {
        public static void IsIP(string IP)
        {
            IPAddress _IP;
            if (string.IsNullOrEmpty(IP))
            {
                return ;
            }
            try
            {
                _IP = IPAddress.Parse(IP);
            }
            catch (FormatException e)
            {
                throw new System.FormatException(e.Message + " \"" + IP + "\"");
            }
        }
      
        public static void IsMAC(string MAC)
        {
            if (string.IsNullOrEmpty(MAC))
            {
                return;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(MAC, @"(([a-f]|[0-9]|[A-F]){2}\:){5}([a-f]|[0-9]|[A-F]){2}\b"))
            {
                throw new System.FormatException("An invalid MAC address was specified. \"" + MAC + "\" valid format is xx:xx:xx:xx:xx:xx");
            }
        }

        public static void IsFWrule(firewallRuleRequest[] Request)
        {
            // not implemented yet
        }

    }
    #endregion

    #region Static_PBApiServive
    public static class PBApi
    {
            public static ProfitbricksApiServicePortTypeClient Service;
    }
    #endregion

    #region Open-PBApiServive
    [Cmdlet(VerbsCommon.Open, "PBApiService")]
    public class Open_PBApiService : PSCmdlet
    {
        [Parameter(
            ParameterSetName = "UserPass",
            Position = 0, 
            Mandatory = true
        )]
        public string Username;

        [Parameter(
            ParameterSetName = "UserPass",
            Position = 1,
            Mandatory = true
        )]
        public string Password;

        [Parameter(
            ParameterSetName = "PSCredentials",
            Position = 0,
            Mandatory = true
        )]
        public PSCredential Credentials;

        protected override void ProcessRecord()
        {
            EndpointAddress EA = new EndpointAddress("https://api.profitbricks.com/1.2");
            // We want to use Basic Auth via SSL to the Webservice
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.Security.Mode = BasicHttpSecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            // assin to the statc Class 
            PBApi.Service  = new ProfitbricksApiServicePortTypeClient(binding, EA);
            // Set Credidentials

            switch (ParameterSetName)
            {
                case "UserPass":
                    PBApi.Service.ClientCredentials.UserName.UserName = Username;
                    PBApi.Service.ClientCredentials.UserName.Password = Password;
                    break;

                case "PSCredentials":
                    PBApi.Service.ClientCredentials.UserName.UserName = Credentials.UserName;
                    // convert PSCredentiasl.password to decrypted password string
                    PBApi.Service.ClientCredentials.UserName.Password = Marshal.PtrToStringBSTR(
                        Marshal.SecureStringToBSTR(Credentials.Password)
                    );
                break;
            }
            // hustvreturens true
            this.WriteObject(Username);
        }
    }
    #endregion

    #region Close-PBApiServive
    [Cmdlet(VerbsCommon.Close, "PBApiService")]
    public class Close_PBApiService : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            EndpointAddress EA = new EndpointAddress("https://api.profitbricks.com/1.2");
            // We want to use Basic Auth via SSL to the Webservice
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.Security.Mode = BasicHttpSecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            // assin to the statc Class 

            PBApi.Service = new ProfitbricksApiServicePortTypeClient(binding, EA);
            
            this.WriteObject(null);
        }
    }
    #endregion

    #region Get-PBApiServive
    [Cmdlet(VerbsCommon.Get, "PBApiService")]
    public class Get_PBApiService : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service);
        }
    }
    #endregion

}
