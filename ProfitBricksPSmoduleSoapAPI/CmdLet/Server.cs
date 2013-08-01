using ProfitBricksPSmoduleSoapAPI.WsProfitBricksApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace ProfitBricksPSmoduleSoapAPI.CmdLet
{
    #region Get_PBServer
    [Cmdlet(VerbsCommon.Get, "PBServer")]
    public class Get_PBServer : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string serverId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.getServer(serverId));
        }
    }
    #endregion

    #region New_PBServer
    [Cmdlet(VerbsCommon.New, "PBServer")]
    public class New_PBServer : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public int cores;

        [Parameter(
            Position = 1,
            Mandatory = true
        )]
        public int ram;

        [Parameter(
            Position = 2,
            Mandatory = false
        )]
        public string dataCenterId;

        [Parameter(
            Position = 4,
            Mandatory = false
        )]
        public string serverName;

        [Parameter(
            Position = 5,
            Mandatory = false
        )]
        public string bootFromImageId;

        [Parameter(
            Position = 6,
            Mandatory = false
        )]
        public string bootFromStorageId;

        [Parameter(
            Position = 7,
            Mandatory = false
        )]
        public int? lanId;

        [Parameter(
            Position = 8,
            Mandatory = false
        )]
        public bool? internetAccess;

        [Parameter(
            Position = 9,
            Mandatory = false
        )]
        public availabilityZone? availabilityZone;

        [Parameter(
            Position = 10,
            Mandatory = false
        )]
        public osType? osType;

        protected override void ProcessRecord()
        {
            createServerRequest Request = new createServerRequest();
            Request.cores = cores;
            Request.ram = ram;
            Request.dataCenterId = dataCenterId;
            Request.serverName = serverName;
            Request.bootFromImageId = bootFromImageId;
            Request.bootFromStorageId = bootFromStorageId;
            
            if (lanId.HasValue)
            {
                Request.lanId = (int)lanId ;
                Request.lanIdSpecified = true;
            }
            if (internetAccess.HasValue)
            {
                Request.internetAccess = (bool)internetAccess;
            }
            if (availabilityZone.HasValue)
            {
                Request.availabilityZone = (availabilityZone)availabilityZone;
                Request.availabilityZoneSpecified = true ;
            }
            if (osType.HasValue)
            {
                Request.osType = (osType)osType;
                Request.osTypeSpecified = true ;
            }

            this.WriteObject(PBApi.Service.createServer(Request));
        }
    }
    #endregion

    #region Reset_PBServer
    [Cmdlet(VerbsCommon.Reset, "PBServer")]
    public class Reset_PBServer : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string serverId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.resetServer(serverId));
        }
    }
    #endregion

    #region Start_PBServer
    [Cmdlet("Start", "PBServer")]
    public class Start_PBServer : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string serverId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.startServer(serverId));
        }
    }
    #endregion

    #region Stop_PBServer
    [Cmdlet("Stop", "PBServer")]
    public class Stop_PBServer : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string serverId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.stopServer(serverId));
        }
    }
    #endregion

    #region Set_PBServer
    [Cmdlet(VerbsCommon.Set, "PBServer")]
    public class Set_PBServer : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public string serverId;

        [Parameter(
            Position = 1,
            Mandatory = false
        )]

        public string serverName;
        [Parameter(
            Position = 2,
            Mandatory = false
        )]
        public int? cores;

        [Parameter(
            Position = 3,
            Mandatory = false
        )]
        public int? ram;

        [Parameter(
            Position = 4,
            Mandatory = false
        )]
        public string bootFromImageId;

        [Parameter(
            Position = 5,
            Mandatory = false
        )]
        public string bootFromStorageId;

        [Parameter(
            Position = 6,
            Mandatory = false
        )]
        public availabilityZone? availabilityZone;

        [Parameter(
            Position = 7,
            Mandatory = false
        )]
        public osType? osType;

        protected override void ProcessRecord()
        {
            updateServerRequest Request = new updateServerRequest();
            if (
                string.IsNullOrEmpty(serverName) &&
                string.IsNullOrEmpty(bootFromImageId) &&
                string.IsNullOrEmpty(bootFromStorageId) &&
                !availabilityZone.HasValue &&
                !osType.HasValue &&
                !cores.HasValue &&
                !ram.HasValue
                )
            {
                throw new System.ArgumentException("at leat on of the following parameters must have a valid value: serverName, cores, ram, bootFromImageId, bootFromStorageId, availabilityZone, osType");
            }
            Request.serverId = serverId;
            if (cores.HasValue)
            {
                Request.cores = (int)cores;
                Request.coresSpecified = true;
            }
            if (ram.HasValue)
            {
                Request.ram = (int)ram;
                Request.ramSpecified = true;
            }
            Request.serverName = serverName;
            Request.bootFromImageId = bootFromImageId;
            Request.bootFromStorageId = bootFromStorageId;
            if (availabilityZone.HasValue)
            {
                Request.availabilityZone = (availabilityZone)availabilityZone;
                Request.availabilityZoneSpecified = true;
            }
            if (osType.HasValue)
            {
                Request.osType = (osType)osType;
                Request.osTypeSpecified = true ;
            }

            this.WriteObject(PBApi.Service.updateServer(Request));
        }
    }
    #endregion

    #region Remove_PBServer
    [Cmdlet(VerbsCommon.Remove, "PBServer")]
    public class Remove_PBServer : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string serverId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.deleteServer(serverId));
        }
    }
    #endregion

}
