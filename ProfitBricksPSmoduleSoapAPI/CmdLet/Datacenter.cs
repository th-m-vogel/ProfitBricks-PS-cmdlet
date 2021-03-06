﻿using ProfitBricksPSmoduleSoapAPI.WsProfitBricksApi;
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
            this.WriteObjects(PBApi.Service.getAllDataCenters());
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
            this.WriteObject(PBApi.Service.getDataCenter(dataCenterId));
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
            this.WriteObject(PBApi.Service.getDataCenterState(dataCenterId));
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
            this.WriteObject(PBApi.Service.clearDataCenter(dataCenterId));
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
            this.WriteObject(PBApi.Service.deleteDataCenter(dataCenterId));
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
        public location Location;

        protected override void ProcessRecord()
        {
            createDataCenterRequest Request = new createDataCenterRequest();

            Request.dataCenterName = dataCenterName;
            Request.location = Location;

            this.WriteVerbose("Create Datacenter: " + dataCenterName + " at Location " + Location);

            var response = PBApi.Service.createDataCenter(Request);
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
            Mandatory = true
        )]
        public string dataCenterName;

        updateDcRequest Request = new updateDcRequest();
        protected override void ProcessRecord()
        {
            Request.dataCenterId = dataCenterId;
            Request.dataCenterName = dataCenterName;

            this.WriteObject(PBApi.Service.updateDataCenter(Request));
        }
    }
    #endregion

}
