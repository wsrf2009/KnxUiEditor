using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.TypesN8.ActuatorConnectType;
using UIEditor.KNX.DatapointType.TypesN8.ADAType;
using UIEditor.KNX.DatapointType.TypesN8.AddInfoTypes;
using UIEditor.KNX.DatapointType.TypesN8.AlarmClassType;
using UIEditor.KNX.DatapointType.TypesN8.ApplicationArea;
using UIEditor.KNX.DatapointType.TypesN8.BackupMode;
using UIEditor.KNX.DatapointType.TypesN8.BeaufortWindForceScale;
using UIEditor.KNX.DatapointType.TypesN8.BehaviourBusPowerUpDown;
using UIEditor.KNX.DatapointType.TypesN8.BehaviourLockUnlock;
using UIEditor.KNX.DatapointType.TypesN8.BlindsControlMode;
using UIEditor.KNX.DatapointType.TypesN8.BlinkingMode;
using UIEditor.KNX.DatapointType.TypesN8.BuildingMode;
using UIEditor.KNX.DatapointType.TypesN8.BurnerType;
using UIEditor.KNX.DatapointType.TypesN8.ChangeoverMode;
using UIEditor.KNX.DatapointType.TypesN8.CommMode;
using UIEditor.KNX.DatapointType.TypesN8.DALIFadeTime;
using UIEditor.KNX.DatapointType.TypesN8.DamperMode;
using UIEditor.KNX.DatapointType.TypesN8.DHWMode;
using UIEditor.KNX.DatapointType.TypesN8.DimmPBModel;
using UIEditor.KNX.DatapointType.TypesN8.ErrorClassHVAC;
using UIEditor.KNX.DatapointType.TypesN8.ErrorClassSystem;
using UIEditor.KNX.DatapointType.TypesN8.FanMode;
using UIEditor.KNX.DatapointType.TypesN8.FuelType;
using UIEditor.KNX.DatapointType.TypesN8.HeaterMode;
using UIEditor.KNX.DatapointType.TypesN8.HVACContrMode;
using UIEditor.KNX.DatapointType.TypesN8.HVACEmergMode;
using UIEditor.KNX.DatapointType.TypesN8.HVACMode;
using UIEditor.KNX.DatapointType.TypesN8.LightApplicationMode;
using UIEditor.KNX.DatapointType.TypesN8.LightControlMode;
using UIEditor.KNX.DatapointType.TypesN8.LoadPriority;
using UIEditor.KNX.DatapointType.TypesN8.LoadTypeDetected;
using UIEditor.KNX.DatapointType.TypesN8.LoadTypeSet;
using UIEditor.KNX.DatapointType.TypesN8.MasterSlaveMode;
using UIEditor.KNX.DatapointType.TypesN8.MeteringDeviceType;
using UIEditor.KNX.DatapointType.TypesN8.OccMode;
using UIEditor.KNX.DatapointType.TypesN8.PBAction;
using UIEditor.KNX.DatapointType.TypesN8.Priority;
using UIEditor.KNX.DatapointType.TypesN8.PSUMode;
using UIEditor.KNX.DatapointType.TypesN8.RFFilterSelect;
using UIEditor.KNX.DatapointType.TypesN8.RFModeSelect;
using UIEditor.KNX.DatapointType.TypesN8.SABBehaviourLockUnlock;
using UIEditor.KNX.DatapointType.TypesN8.SABExceptBehaviour;
using UIEditor.KNX.DatapointType.TypesN8.SCLOMode;
using UIEditor.KNX.DatapointType.TypesN8.SensorSelect;
using UIEditor.KNX.DatapointType.TypesN8.SSSBMode;
using UIEditor.KNX.DatapointType.TypesN8.StartSynchronization;
using UIEditor.KNX.DatapointType.TypesN8.StatusRoomSetp;
using UIEditor.KNX.DatapointType.TypesN8.SwitchOnMode;
using UIEditor.KNX.DatapointType.TypesN8.SwitchPBModel;
using UIEditor.KNX.DatapointType.TypesN8.TimeDelay;
using UIEditor.KNX.DatapointType.TypesN8.ValveMode;

namespace UIEditor.KNX.DatapointType.TypesN8
{
    class TypesN8Node:DatapointType
    {
        public TypesN8Node()
        {
            this.KNXMainNumber = DPT_20;
            this.Name = "1-byte";
            this.Type = KNXDataType.Bit8;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypesN8Node nodeType = new TypesN8Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(SCLOModeNode.GetTypeNode());
            nodeType.Nodes.Add(BuildingModeNode.GetTypeNode());
            nodeType.Nodes.Add(OccModeNode.GetTypeNode());
            nodeType.Nodes.Add(PriorityNode.GetTypeNode());
            nodeType.Nodes.Add(LightApplicationModeNode.GetTypeNode());
            nodeType.Nodes.Add(ApplicationAreaNode.GetTypeNode());
            nodeType.Nodes.Add(AlarmClassTypeNode.GetTypeNode());
            nodeType.Nodes.Add(PSUModeNode.GetTypeNode());
            nodeType.Nodes.Add(ErrorClassSystemNode.GetTypeNode());
            nodeType.Nodes.Add(ErrorClassHVACNode.GetTypeNode());
            nodeType.Nodes.Add(TimeDelayNode.GetTypeNode());
            nodeType.Nodes.Add(BeaufortWindForceScaleNode.GetTypeNode());
            nodeType.Nodes.Add(SensorSelectNode.GetTypeNode());
            nodeType.Nodes.Add(ActuatorConnectTypeNode.GetTypeNode());
            nodeType.Nodes.Add(FuelTypeNode.GetTypeNode());
            nodeType.Nodes.Add(BurnerTypeNode.GetTypeNode());
            nodeType.Nodes.Add(HVACModeNode.GetTypeNode());
            nodeType.Nodes.Add(DHWModeNode.GetTypeNode());
            nodeType.Nodes.Add(LoadPriorityNode.GetTypeNode());
            nodeType.Nodes.Add(HVACContrModeNode.GetTypeNode());
            nodeType.Nodes.Add(HVACEmergModeNode.GetTypeNode());
            nodeType.Nodes.Add(ChangeoverModeNode.GetTypeNode());
            nodeType.Nodes.Add(ValveModeNode.GetTypeNode());
            nodeType.Nodes.Add(DamperModeNode.GetTypeNode());
            nodeType.Nodes.Add(HeaterModeNode.GetTypeNode());
            nodeType.Nodes.Add(FanModeNode.GetTypeNode());
            nodeType.Nodes.Add(MasterSlaveModeNode.GetTypeNode());
            nodeType.Nodes.Add(StatusRoomSetpNode.GetTypeNode());
            nodeType.Nodes.Add(MeteringDeviceTypeNode.GetTypeNode());
            nodeType.Nodes.Add(ADATypeNode.GetTypeNode());
            nodeType.Nodes.Add(BackupModeNode.GetTypeNode());
            nodeType.Nodes.Add(StartSynchronizationNode.GetTypeNode());
            nodeType.Nodes.Add(BehaviourLockUnlockNode.GetTypeNode());
            nodeType.Nodes.Add(BehaviourBusPowerUpDownNode.GetTypeNode());
            nodeType.Nodes.Add(DALIFadeTimeNode.GetTypeNode());
            nodeType.Nodes.Add(BlinkingModeNode.GetTypeNode());
            nodeType.Nodes.Add(LightControlModeNode.GetTypeNode());
            nodeType.Nodes.Add(SwitchPBModelNode.GetTypeNode());
            nodeType.Nodes.Add(PBActionNode.GetTypeNode());
            nodeType.Nodes.Add(DimmPBModelNode.GetTypeNode());
            nodeType.Nodes.Add(SwitchOnModeNode.GetTypeNode());
            nodeType.Nodes.Add(LoadTypeSetNode.GetTypeNode());
            nodeType.Nodes.Add(LoadTypeDetectedNode.GetTypeNode());
            nodeType.Nodes.Add(SABExceptBehaviourNode.GetTypeNode());
            nodeType.Nodes.Add(SABBehaviourLockUnlockNode.GetTypeNode());
            nodeType.Nodes.Add(SSSBModeNode.GetTypeNode());
            nodeType.Nodes.Add(BlindsControlModeNode.GetTypeNode());
            nodeType.Nodes.Add(CommModeNode.GetTypeNode());
            nodeType.Nodes.Add(AddInfoTypesNode.GetTypeNode());
            nodeType.Nodes.Add(RFModeSelectNode.GetTypeNode());
            nodeType.Nodes.Add(RFFilterSelectNode.GetTypeNode());

            return nodeType;
        }
    }
}
