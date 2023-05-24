﻿using Application.Stores;

namespace Infrastructure.Stores;
/// <summary>
/// Has a hashset of all available area codes of the Netherlands
/// </summary>
internal class AreaCodesNetherlands : IAreaCodesStore
{
    #region Private fields
    private HashSet<string> _areaCodes = new()
    {
        "297","72","546","36","172","33","20","55","26","592",
        "180","342","593","481","164","499","251","411","76","524",
        "345","485","529","15","596","222","223","493","570","313",
        "314","519","78","512","343","321","487","497","545","40",
        "525","577","527","591","228","53","578","113","547","183",
        "182","486","50","566","23","523","341","517","513","45",
        "492","74","73","35","528","229","114","255","58","71",
        "320","514","544","573","346","43","227","522","118","187",
        "174","252","24","226","541","117","162","516","412","186",
        "299","572","161","548","475","165","10","224","46","184",
        "515","181","518","599","167","521","315","115","70","166",
        "344","13","571","413","30","598","318","511","77","478",
        "347","416","317","595","495","294","562","597","543","348",
        "561","75","418","488","316","168","111","79","594","575",
        "38"
    };
    #endregion


    #region Public methods
    public bool IsValidCode(string code) => _areaCodes.Contains(code);
    #endregion
}