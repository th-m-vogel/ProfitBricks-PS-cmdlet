using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ProfitBricksPSmoduleSoapAPI.WsProfitBricksApi;
using System.Management.Automation;
using System.Collections;
using System.Runtime.InteropServices;

namespace ProfitBricksPSmoduleSoapAPI
{
    #region CommonCode

    public class PBHelper : PSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public ProfitbricksApiServicePortTypeClient PBApiService;

        protected void WriteObjects(IEnumerable objects)
        {
            foreach (var obj in objects)
            {
                WriteObject(obj);
            }
        }
    }


    #endregion
    #region New-PBApiServive
    [Cmdlet(VerbsCommon.New, "PBApiService")]
    public class New_PBApiService : PSCmdlet
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
            // create a new Basic HTTP Binding
            BasicHttpBinding binding = new BasicHttpBinding();
            // set transport mode security (https)
            binding.Security.Mode = BasicHttpSecurityMode.Transport;
            // set authentication type to Basic
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            // set SOAP Interface URL
            EndpointAddress EA = new EndpointAddress("https://api.profitbricks.com/1.2");
            // create API Client
            var PBApi = new ProfitbricksApiServicePortTypeClient(binding, EA);
            // Set Credidentials

            switch (ParameterSetName)
            {
                case "UserPass":
                    PBApi.ClientCredentials.UserName.UserName = Username;
                    PBApi.ClientCredentials.UserName.Password = Password;
                    break;

                case "PSCredentials":
                    PBApi.ClientCredentials.UserName.UserName = Credentials.UserName;
                    // convert PSCredentiasl.password to decrypted password string
                    PBApi.ClientCredentials.UserName.Password = Marshal.PtrToStringBSTR(
                        Marshal.SecureStringToBSTR(Credentials.Password));
                break;
            }

            this.WriteObject(PBApi);
        }
    }
    #endregion

    #region Get-PBDatacenterIdentifiers
    [Cmdlet(VerbsCommon.Get, "DatacenterIdentifiers")]
    public class Get_DatacenterIdentifiers : PBHelper
    {

        protected override void ProcessRecord()
        {
            this.WriteObjects(PBApiService.getAllDataCenters());
        }
    }
    #endregion

    #region Get-PBDatacenter
    [Cmdlet(VerbsCommon.Get, "PBDatacenter")]
    public class Get_Datacenter : PBHelper
    {

        [Parameter(
            Position = 1,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string dataCenterId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApiService.getDataCenter(dataCenterId));
        }
    }
    #endregion

    #region Get_PBDatacenterState
    [Cmdlet(VerbsCommon.Get, "PBDatacenterState")]
    public class Get_DatacenterStatus : PBHelper
    {

        [Parameter(
            Position = 1,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string dataCenterId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApiService.getDataCenterState(dataCenterId));
        }
    }
    #endregion 

    #region New-PBDatacenter
    [Cmdlet(VerbsCommon.New, "PBDatacenter")]
    public class New_Datacenter : PBHelper
    {
        [Parameter(
            Position = 1,
            Mandatory = false
        )]
        public string dataCenterName;

        [Parameter(
            Position = 2,
            Mandatory = false
        )]
        public region Region;

        protected override void ProcessRecord()
        {
            this.WriteVerbose("Create Datacenter: " + dataCenterName + " in Region " + Region);
            var response = PBApiService.createDataCenter(dataCenterName, Region);
            this.WriteVerbose("RequestID " + response.requestId + " created Dataceter using UUID " + response.dataCenterId);
        }
    }
    #endregion
}
