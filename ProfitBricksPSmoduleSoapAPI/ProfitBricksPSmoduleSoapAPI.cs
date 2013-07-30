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
            foreach (var obj in objects)
            {
                WriteObject(obj);
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
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(MAC, @"(([a-f]|[0-9]|[A-F]){2}\:){5}([a-f]|[0-9]|[A-F]){2}\b"))
                {
                    throw new System.FormatException("An invalid MAC address was specified. \"" + MAC + "\" valid format is xx:xx:xx:xx:xx:xx");
                }
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

        [Parameter(
            Mandatory = false
        )]
        public string WsUri;


        protected override void ProcessRecord()
        {
            if (string.IsNullOrEmpty(WsUri))
            {
                WsUri = "https://api.profitbricks.com/1.2";
            }
            EndpointAddress EA = new EndpointAddress(WsUri);
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
            PBApi.Service.ClientCredentials.UserName.UserName = "";
            PBApi.Service.ClientCredentials.UserName.Password = "";
            
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
