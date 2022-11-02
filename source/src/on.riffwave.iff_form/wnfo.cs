/*
 * tfooo in #DEVLEOP
 * DateTime: 12/27/2006 : 9:04 AM
 */

using System;

namespace on.riffwave.iff_form
{
  static class ListTags
  {
    internal const uint ArchivalLocation = 0x4941524C;  //  "IARL"
    internal const uint Artist           = 0x49415254;  //  "IART"
    internal const uint Commissioned     = 0x49434D53;  //  "ICMS"
    internal const uint Comment          = 0x49434D54;  //  "ICMT"
    internal const uint Copyright        = 0x49434F50;  //  "ICOP"
    internal const uint DateCreated      = 0x49435244;  //  "ICRD"
    internal const uint Cropped          = 0x49435250;  //  "ICRP"
    internal const uint Dimensions       = 0x4944494D;  //  "IDIM"
    internal const uint DotsPerInch      = 0x49445049;  //  "IDPI"
    internal const uint Engineer         = 0x49454E47;  //  "IENG"
    internal const uint Genre            = 0x49474E52;  //  "IGNR"
    internal const uint Keywords         = 0x494B4559;  //  "IKEY"
    internal const uint Lightness        = 0x494C4754;  //  "ILGT"
    internal const uint Medium           = 0x494D4544;  //  "IMED"
    internal const uint Title            = 0x494E414D;  //  "INAM"
    internal const uint NumColors        = 0x49504C54;  //  "IPLT"
    internal const uint Product          = 0x49505244;  //  "IPRD"
    internal const uint Subject          = 0x4953424A;  //  "ISBJ"
    internal const uint Software         = 0x49534654;  //  "ISFT"
    internal const uint Sharpness        = 0x49534850;  //  "ISHP"
    internal const uint Source           = 0x49535243;  //  "ISRC"
    internal const uint SourceForm       = 0x49535246;  //  "ISRF"
    internal const uint Technician       = 0x49544348;  //  "ITCH"
  }
  public enum  wnfo : uint
  {
    ArchivalLocation = ListTags.ArchivalLocation , //  "IARL"
    Artist           = ListTags.Artist           , //  "IART"
    Commissioned     = ListTags.Commissioned     , //  "ICMS"
    Comment          = ListTags.Comment          , //  "ICMT"
    Copyright        = ListTags.Copyright        , //  "ICOP"
    DateCreated      = ListTags.DateCreated      , //  "ICRD"
    Cropped          = ListTags.Cropped          , //  "ICRP"
    Dimensions       = ListTags.Dimensions       , //  "IDIM"
    DotsPerInch      = ListTags.DotsPerInch      , //  "IDPI"
    Engineer         = ListTags.Engineer         , //  "IENG"
    Genre            = ListTags.Genre            , //  "IGNR"
    Keywords         = ListTags.Keywords         , //  "IKEY"
    Lightness        = ListTags.Lightness        , //  "ILGT"
    Medium           = ListTags.Medium           , //  "IMED"
    Title            = ListTags.Title            , //  "INAM"
    NumColors        = ListTags.NumColors        , //  "IPLT"
    Product          = ListTags.Product          , //  "IPRD"
    Subject          = ListTags.Subject          , //  "ISBJ"
    Software         = ListTags.Software         , //  "ISFT"
    Sharpness        = ListTags.Sharpness        , //  "ISHP"
    Source           = ListTags.Source           , //  "ISRC"
    SourceForm       = ListTags.SourceForm       , //  "ISRF"
    Technician       = ListTags.Technician       ,  //  "ITCH"
  }
}
