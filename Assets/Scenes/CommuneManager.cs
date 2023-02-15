using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommuneManager : MonoBehaviour
{
    private const string _communes = "Albigny-sur-Saône\r\nBron\r\nCailloux-sur-Fontaines\r\nCaluire-et-Cuire\r\nChampagne-au-Mont-d'Or\r\nCharbonnières-les-Bains\r\nCharly\r\nChassieu\r\nCollonges-au-Mont-d'Or\r\nCorbas\r\nCouzon-au-Mont-d'Or\r\nCraponne\r\nCuris-au-Mont-d'Or\r\nDardilly\r\nDécines-Charpieu\r\nÉcully\r\nFeyzin\r\nFleurieu-sur-Saône\r\nFontaines-Saint-Martin\r\nFontaines-sur-Saône\r\nFrancheville\r\nGenay\r\nGivors\r\nGrigny\r\nIrigny\r\nJonage\r\nLa Mulatière\r\nLa Tour de Salvagny\r\nLimonest\r\nLissieu\r\nLyon\r\nLyon 1er arrondissement\r\nLyon 2e arrondissement\r\nLyon 3e arrondissement\r\nLyon 4e arrondissement\r\nLyon 5e arrondissement\r\nLyon 6e arrondissement\r\nLyon 7e arrondissement\r\nLyon 8e arrondissement\r\nLyon 9e arrondissement\r\nMarcy-l'Etoile\r\nMeyzieu\r\nMions\r\nMontanay\r\nNeuville-sur-Saône\r\nOullins\r\nPierre-Bénite\r\nPoleymieux-au-Mont-d'Or\r\nQuincieux\r\nRillieux-la-Pape\r\nRochetaillée-sur-Saône\r\nSaint-Cyr-au-Mont-d'Or\r\nSaint-Didier-au-Mont-d'Or\r\nSaint-Fons\r\nSaint-Genis-Laval\r\nSaint-Genis-les-Ollières\r\nSaint-Germain-au-Mont-d'Or\r\nSaint-Priest\r\nSaint-Romain-au-Mont-d'Or\r\nSainte-Foy-lès-Lyon\r\nSathonay-Camp\r\nSathonay-Village\r\nSolaize\r\nTassin-la-Demi-Lune\r\nVaulx-en-Velin\r\nVénissieux\r\nVernaison\r\nVilleurbanne";
    [SerializeField]
    private string[] _communeTab;
    [SerializeField]
    private TMP_Dropdown _communesDropdown;
    [ContextMenu(nameof(Convert))]
    private void Convert()
    {
        var splitted = _communes.Replace("\r\n", "\n").Split('\n');
        var l = new List<string>();
        l.Add("Commune");
        l.AddRange(splitted);
        _communeTab = l.ToArray();
    }
    [ContextMenu(nameof(FillDropdown))]
    private void FillDropdown()
    {
        _communesDropdown.options = _communeTab.Select(s => new TMP_Dropdown.OptionData(s)).ToList();
    }
}
