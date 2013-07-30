using ProfitBricksPSmoduleSoapAPI.WsProfitBricksApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace ProfitBricksPSmoduleSoapAPI.CmdLet
{
    #region New_PBLoadBalancer
    [Cmdlet(VerbsCommon.New, "PBLoadBalancer")]
    public class New_PBLoadBalancer : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public string dataCenterId;

        [Parameter(
            Position = 1,
            Mandatory = false
        )]
        public string loadBalancerName;

        [Parameter(
            Position = 2,
            Mandatory = false
        )]
        public string loadBalancerAlgorithm;

        [Parameter(
            Position = 3,
            Mandatory = false
        )]
        public string ip;

        [Parameter(
            Position = 3,
            Mandatory = false
        )]
        public int lanId;

        [Parameter(
            Position = 3,
            Mandatory = false
        )]
        public string[] serverIds;

        protected override void ProcessRecord()
        {
            PBapiChecks.IsIP(ip);
            createLbRequest Request = new createLbRequest();
            Request.dataCenterId = dataCenterId;
            Request.loadBalancerName = loadBalancerName;
            //// If string value spezified is a valid enum
            //// set Request.ParemeterSpecified and Request.Parameter
            //if (!(string.IsNullOrEmpty(loadBalancerAlgorithm)))
            //{
            //    if ((Request.loadBalancerAlgorithmSpecified = Enum.IsDefined(typeof(loadBalancerAlgorithm), loadBalancerAlgorithm.ToUpper())))
            //    {
            //        Request.loadBalancerAlgorithm = (loadBalancerAlgorithm)Enum.Parse(typeof(loadBalancerAlgorithm), loadBalancerAlgorithm.ToUpper());
            //    }
            //}
            // Not Implemented yet, there does only exist the default ROUND_ROBIN algorithm
            //
            Request.ip = ip;
            if (lanId > 0)
            {
                Request.lanId = lanId;
                Request.lanIdSpecified = true;
            } 
            Request.serverIds = serverIds;
            this.WriteObject(PBApi.Service.createLoadBalancer(Request));
        }
    }
    #endregion

    #region Get_PBLoadBalancer
    [Cmdlet(VerbsCommon.Get, "PBLoadBalancer")]
    public class Get_PBLoadBalancer : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string loadBalancerId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.getLoadBalancer(loadBalancerId));
        }
    }
    #endregion

    #region Set_PBLoadBalancer
    [Cmdlet(VerbsCommon.Set, "PBLoadBalancer")]
    public class Set_PBLoadBalancer : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public string loadBalancerId;

        [Parameter(
            Position = 1,
            Mandatory = false
        )]
        public string loadBalancerName;

        [Parameter(
            Position = 2,
            Mandatory = false
        )]
        public string loadBalancerAlgorithm;

        [Parameter(
            Position = 3,
            Mandatory = false
        )]
        public string ip;

        protected override void ProcessRecord()
        {
            updateLbRequest Request = new updateLbRequest();
            if (
                string.IsNullOrWhiteSpace(loadBalancerName) &&
                // string.IsNullOrWhiteSpace(loadBalancerAlgorithm) &&
                string.IsNullOrWhiteSpace(ip)
                )
            {
                // Algorithm is not implemented yet, ther is only the default algorithm
                //throw new System.ArgumentException("at leat on of the following parameters must have a valid value: loadBalancerName, loadBalancerAlgorithm, ip");
                throw new System.ArgumentException("at leat on of the following parameters must have a valid value: loadBalancerName, ip");
            }
            PBapiChecks.IsIP(ip);
            Request.loadBalancerId = loadBalancerId;
            Request.loadBalancerName = loadBalancerName;
            //// If string value spezified is a valid enum
            //// set Request.ParemeterSpecified and Request.Parameter
            //if (!(string.IsNullOrEmpty(loadBalancerAlgorithm)))
            //{
            //    if ((Request.loadBalancerAlgorithmSpecified = Enum.IsDefined(typeof(loadBalancerAlgorithm), loadBalancerAlgorithm.ToUpper())))
            //    {
            //        Request.loadBalancerAlgorithm = (loadBalancerAlgorithm)Enum.Parse(typeof(loadBalancerAlgorithm), loadBalancerAlgorithm.ToUpper());
            //    }
            //}
            // Not Implemented yet, there does only exist the default ROUND_ROBIN algorithm
            //
            Request.ip = ip;
            this.WriteObject(PBApi.Service.updateLoadBalancer(Request));
        }
    }
    #endregion

    #region Connect_PBServerToLoadBalancer
    [Cmdlet("Connect", "PBServerToLoadBalancer")]
    public class Connect_PBServerToLoadBalancer : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public string[] serverIds;

        [Parameter(
            Position = 1,
            Mandatory = false
        )]
        public string loadBalancerId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.registerServersOnLoadBalancer(serverIds, loadBalancerId));
        }
    }
    #endregion

    #region Remove_PBServerFromLoadBalancer
    [Cmdlet(VerbsCommon.Remove, "PBServerFromLoadBalancer")]
    public class Remove_PBServerFromLoadBalancer : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public string[] serverIds;

        [Parameter(
            Position = 1,
            Mandatory = false
        )]
        public string loadBalancerId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.deregisterServersOnLoadBalancer(serverIds, loadBalancerId));
        }
    }
    #endregion

    #region Remove_PBLoadBalancer
    [Cmdlet(VerbsCommon.Remove, "PBLoadBalancer")]
    public class Remove_PBLoadBalancer : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string loadBalancerId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.deleteLoadBalancer(loadBalancerId));
        }
    }
    #endregion
}
