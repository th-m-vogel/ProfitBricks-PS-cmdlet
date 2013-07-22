﻿using ProfitBricksPSmoduleSoapAPI.WsProfitBricksApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ProfitBricksPSmoduleSoapAPI
{
    #region PBapiPSCmdlet
    public class PBapiPSCmdlet : PSCmdlet
    {
        protected void WriteObjects(IEnumerable objects)
        {
            foreach (var obj in objects)
            {
                WriteObject(obj);
            }
        }
    }
    #endregion

    #region Static_PBApiServive
    public static class PBApiServive
    {
        public static ProfitbricksApiServicePortTypeClient Servive;
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
            // We want to use Basic Auth via SSL to the Webservice
            EndpointAddress EA = new EndpointAddress("https://api.profitbricks.com/1.2");
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.Security.Mode = BasicHttpSecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            // assin to the statc Class 
            PBApiServive.Servive  = new ProfitbricksApiServicePortTypeClient(binding, EA);
            // Set Credidentials

            switch (ParameterSetName)
            {
                case "UserPass":
                    PBApiServive.Servive.ClientCredentials.UserName.UserName = Username;
                    PBApiServive.Servive.ClientCredentials.UserName.Password = Password;
                    break;

                case "PSCredentials":
                    PBApiServive.Servive.ClientCredentials.UserName.UserName = Credentials.UserName;
                    // convert PSCredentiasl.password to decrypted password string
                    PBApiServive.Servive.ClientCredentials.UserName.Password = Marshal.PtrToStringBSTR(
                        Marshal.SecureStringToBSTR(Credentials.Password)
                    );
                break;
            }
            // hustvreturens true
            this.WriteObject(true);
        }
    }
    #endregion
}
