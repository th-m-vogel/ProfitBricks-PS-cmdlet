using ProfitBricksPSmoduleSoapAPI.WsProfitBricksApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace ProfitBricksPSmoduleSoapAPI.CmdLet
{
    #region New_PBFirewallRulesToNic
    [Cmdlet(VerbsCommon.New, "PBFirewallRulesToNic")]
    public class New_PBFirewallRulesToNic : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public string nicId;

        [Parameter(
            Position = 1,
            Mandatory = false
        )]
        public string firewallRuleName;

        [Parameter(
            Position = 2,
            Mandatory = false
        )]
        public protocol protocol;

        [Parameter(
            Position = 3,
            Mandatory = false
        )]
        public string sourceMac;

        [Parameter(
            Position = 4,
            Mandatory = false
        )]
        public string sourceIp;

        [Parameter(
            Position = 5,
            Mandatory = false
        )]
        public string targetIp;

        [Parameter(
            Position = 6,
            Mandatory = false
        )]
        public int portRangeStart;

        [Parameter(
            Position = 7,
            Mandatory = false
        )]
        public int portRangeEnd;

        [Parameter(
            Position = 8,
            Mandatory = false
        )]
        public int icmpType;

        [Parameter(
            Position = 9,
            Mandatory = false
        )]
        public int icmpCode;

        protected override void ProcessRecord()
        {
            firewallRuleRequest[] request = {new firewallRuleRequest()};
            if (icmpCode > 0) {
                request[0].icmpCode = icmpCode;
                request[0].icmpCodeSpecified = true;
            }
            if (icmpType > 0) {
                request[0].icmpType = icmpType; 
                request[0].icmpTypeSpecified = true;
            }
            if ((portRangeEnd > 0) && (portRangeEnd > portRangeStart))
            {
                request[0].portRangeEnd = portRangeEnd;
                request[0].portRangeEndSpecified = true;
                request[0].portRangeStart = portRangeStart;
                request[0].portRangeStartSpecified = true;
            }
            request[0].sourceIp = sourceIp;
            request[0].targetIp = targetIp;
            request[0].sourceMac = sourceMac;
            request[0].protocol = protocol;
            // request. = firewallRuleName;
            
            this.WriteObject(PBApi.Servive.addFirewallRulesToNic(request, nicId));
        }
    }
    #endregion

    #region Get_PBFirewall
    [Cmdlet(VerbsCommon.Get, "PBFirewall")]
    public class Get_PBFirewall : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string firewallId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Servive.getFirewall(firewallId));
        }
    }
    #endregion

    #region Remove_PBFirewallRules
    [Cmdlet(VerbsCommon.Remove, "PBFirewallRules")]
    public class Remove_PBFirewallRules : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string[] firewallRuleIds;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Servive.removeFirewallRules(firewallRuleIds));
        }
    }
    #endregion

    #region Switch_PBFirewallStatus
    [Cmdlet(VerbsCommon.Switch, "PBFirewallStatus")]
    public class Switch_PBFirewallStatus : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string[] firewallIds;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Servive.activateFirewalls(firewallIds));
        }
    }
    #endregion

    #region Remove_PBFirewalls
    [Cmdlet(VerbsCommon.Switch, "PBFirewalls")]
    public class Remove_PBFirewalls : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string[] firewallIds;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Servive.deleteFirewalls(firewallIds));
        }
    }
    #endregion


}
