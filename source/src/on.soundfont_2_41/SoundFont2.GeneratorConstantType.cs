/* tfooo with #develop */

using System;

namespace on.soundfont_2_41
{
  public partial class SoundFont2
  {
    public TGenValue GetGenValueType(int indexIGEN)
    {
      return GetGenValueType(this.hyde.igen[indexIGEN].Generator);
    }
    /// <summary>
    /// This method has not been used.
    /// Get the type of generator value.
    /// This value is provided by the IGEN hydra-table.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    static public TGenValue GetGenValueType(SFGenConst value)
    {
      switch (value)
      {
        case SFGenConst.keyRange:
          return TGenValue.midiKeyRange;
        
        case SFGenConst.releaseVolEnv:
        case SFGenConst.sustainModEnv:
          return TGenValue.timeCent;
        
        case SFGenConst.delayModLFO:
        case SFGenConst.delayVibLFO:
        case SFGenConst.delayModEnv:
        case SFGenConst.attackModEnv:
        case SFGenConst.holdModEnv:
        case SFGenConst.decayModEnv:
        case SFGenConst.releaseModEnv:
        case SFGenConst.delayVolEnv:
        case SFGenConst.attackVolEnv:
        case SFGenConst.holdVolEnv:
        case SFGenConst.decayVolEnv:
          return TGenValue.absoluteTimeCents; // I think this section is complete
        
        case SFGenConst.sampleModes:
          return TGenValue.sampleMode;
        
        case SFGenConst.startAddrsOffset:
        case SFGenConst.endAddrsOffset:
        case SFGenConst.startloopAddrsOffset:
        case SFGenConst.startLoopAddrsCoarseOffs:
        case SFGenConst.endloopAddrsOffset:
        case SFGenConst.endloopAddrsCoarseOffset:
          return TGenValue.smpls; // this section is complete
        
        case SFGenConst.startAddrsCoarseOffset:
        case SFGenConst.endAddrsCoarseOffset:
          return TGenValue.smpls32k;
        
        case SFGenConst.modLfoToPitch:
        case SFGenConst.vibLfoToPitch:
        case SFGenConst.modEnvToPitch:
        case SFGenConst.modLfoToFilterFc:
        case SFGenConst.modEnvToFilterFc:
          return TGenValue.centsFs;
        
        case SFGenConst.initialFilterFc:
        case SFGenConst.freqModLFO:
        case SFGenConst.freqVibLFO:
          return TGenValue.absoluteCents;
        
        case SFGenConst.fineTune:
          return TGenValue.cent;
        
        case SFGenConst.initialFilterQ:
        case SFGenConst.initialAttenuation:
          return TGenValue.cB;
        
        case SFGenConst.sustainVolEnv:
          return TGenValue.cbAttn;
          
        case SFGenConst.modLfoToVolume:
          return TGenValue.cB_fs;
        
        case SFGenConst.chorusEffectsSend:
        case SFGenConst.reverbEffectsSend:
        case SFGenConst.pan:
          return TGenValue.percent;
        
        case SFGenConst.sampleID:
          return TGenValue.sampleID;
        
        case SFGenConst.overridingRootKey:
          return TGenValue.midiKey;
        
        default:
          return TGenValue.unsupported;
      }
    }
  }
}
