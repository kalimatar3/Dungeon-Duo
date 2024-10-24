using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "ScriptableObjects/CharacterSO", order = 0)]
public class CharacterSO : ScriptableObject {
    public Sprite Icon;
    public Statistic BaseStatistic;
    public string CharacterDescription;
    public Sprite SkillIcon;
    public string SkillDescription;
}
