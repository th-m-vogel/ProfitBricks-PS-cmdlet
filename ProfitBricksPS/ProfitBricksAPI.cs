using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ProfitBricksPS.WsProfitBricksApi;
using System.Management.Automation;

namespace ProfitBricksPS
{

    #region New-PBApiServive
    [Cmdlet(VerbsCommon.New, "PBApiService")]
    public class New_PBApiService : PSCmdlet
    {
        [Parameter(
            Position = 0, 
            Mandatory = true
        )]
        public string Username;
        [Parameter(
            Position = 1,
            Mandatory = true
        )]
        public string Password;

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
            PBApi.ClientCredentials.UserName.UserName = Username;
            PBApi.ClientCredentials.UserName.Password = Password;

            // PBApi.Open();

            this.WriteObject(PBApi);
        }
    }
    #endregion

    #region Get-PBAllDatacenters
    [Cmdlet(VerbsCommon.Get, "PBAllDataCenters")]
    public class Get_Datacenters : PSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public ProfitbricksApiServicePortTypeClient PBApiService;

        protected override void ProcessRecord()
        {
            foreach(var _dc in PBApiService.getAllDataCenters()) {
                this.WriteObject(_dc);
            }
        }
    }
    #endregion

    #region Get-PBDatacenter
    [Cmdlet(VerbsCommon.Get, "PBDatacenter")]
    public class Get_Datacenter : PSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public ProfitbricksApiServicePortTypeClient PBApiService;

        [Parameter(
            Position = 1,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string[] dataCenterId;

        protected override void ProcessRecord()
        {
            foreach (var id in dataCenterId)
            {
                // this.WriteObject(id);
                this.WriteObject(PBApiService.getDataCenter(id));
            }
        }
    }
    #endregion

    #region New-PBDatacenter
    [Cmdlet(VerbsCommon.New, "PBDatacenter")]
    public class New_Datacenter : PSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public ProfitbricksApiServicePortTypeClient PBApiService;

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
            this.WriteObject(dataCenterName);
            this.WriteObject(Region);
            var response = PBApiService.createDataCenter(dataCenterName, Region);
        }
    }
    #endregion

}
