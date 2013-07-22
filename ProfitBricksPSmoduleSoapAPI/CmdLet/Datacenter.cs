using ProfitBricksPSmoduleSoapAPI.WsProfitBricksApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace ProfitBricksPSmoduleSoapAPI.CmdLet
{

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

}
