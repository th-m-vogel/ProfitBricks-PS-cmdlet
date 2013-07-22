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
    #region PBapiPSCmdlet

    public class PBapiPSCmdlet : PSCmdlet
    {
        //[Parameter(
        //    Position = 0,
        //    Mandatory = true
        //)]
        //public ProfitbricksApiServicePortTypeClient PBApiService;
        

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

    #region DataCenterOperations
    #region Get-PBDatacenterIdentifiers
    [Cmdlet(VerbsCommon.Get, "PBDatacenterIdentifiers")]
    public class Get_DatacenterIdentifiers : PBapiPSCmdlet
    {
        protected override void ProcessRecord()
        {
            this.WriteObjects(PBApiServive.Servive.getAllDataCenters());
        }
    }
    #endregion

    #region Get-PBDatacenter
    [Cmdlet(VerbsCommon.Get, "PBDatacenter")]
    public class Get_Datacenter : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string dataCenterId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApiServive.Servive.getDataCenter(dataCenterId));
        }
    }
    #endregion

    #region Get_PBDatacenterState
    [Cmdlet(VerbsCommon.Get, "PBDatacenterState")]
    public class Get_DatacenterStatus : PBapiPSCmdlet
    {

        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string dataCenterId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApiServive.Servive.getDataCenterState(dataCenterId));
        }
    }
    #endregion 

    #region Clear_PBDatacenter
    [Cmdlet(VerbsCommon.Clear, "PBDataCenter")]
    public class Clear_Datacenter : PBapiPSCmdlet
    {

        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string dataCenterId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApiServive.Servive.clearDataCenter(dataCenterId));
        }
    }
    #endregion 
 
    #region Remove_PBDatacenter
    [Cmdlet(VerbsCommon.Remove, "PBDataCenter")]
    public class Remove_Datacenter : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string dataCenterId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApiServive.Servive.deleteDataCenter(dataCenterId));
        }
    }
    #endregion 

    #region New-PBDatacenter
    [Cmdlet(VerbsCommon.New, "PBDatacenter")]
    public class New_Datacenter : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = false
        )]
        public string dataCenterName;

        [Parameter(
            Position = 1,
            Mandatory = false
        )]
        public region Region;

        protected override void ProcessRecord()
        {
            this.WriteVerbose("Create Datacenter: " + dataCenterName + " in Region " + Region);
            var response = PBApiServive.Servive.createDataCenter(dataCenterName, Region);
            this.WriteVerbose("RequestID " + response.requestId + " created Dataceter using UUID " + response.dataCenterId);
            // return CreateDatacenterResponse
            this.WriteObject(response);
        }
    }
    #endregion

    #region Set-PBDatacenter
    [Cmdlet(VerbsCommon.Set, "PBDatacenter")]
    public class Set_Datacenter : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public string dataCenterId;

        [Parameter(
            Mandatory = false
        )]
        public string dataCenterName;

        updateDcRequest Request = new updateDcRequest();
        protected override void ProcessRecord()
        {
            Request.dataCenterId = dataCenterId;
            Request.dataCenterName = dataCenterName;

            this.WriteObject(PBApiServive.Servive.updateDataCenter(Request));
        }
    }
    #endregion

    #endregion // DataCenter Operations

    #region Notifications
    #region Get_PBNotifications
    [Cmdlet(VerbsCommon.Get, "PBNotifications")]
    public class Get_Notifications : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string dataCenterId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApiServive.Servive.getNotifications(dataCenterId));
        }
    }
    #endregion 
    #region Remove_PBNotifications
    [Cmdlet(VerbsCommon.Remove, "PBNotifications")]
    public class Remove_Notifications : PBapiPSCmdlet
    {

        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string[] notificationsId;

        protected override void ProcessRecord()
        {
            PBApiServive.Servive.deleteNotifications(notificationsId);
        }
    }
    #endregion 
    #endregion // Notification Operations


}
