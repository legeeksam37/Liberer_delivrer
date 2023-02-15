using System.Collections.Generic;
using Unisave.Entities;

public class GameDataEntity : Entity
{
    public List<QuizResult> quizResults;
    public List<MissionResult> missionResults;
}

public class MissionResult
{
    public int? travelMethodIndex;
    public int? delayTypeIndex;
    public int? withdrawalTypeIndex;
    public int? storeType;
}

public class QuizResult
{
    public int answerIndex;
}
