using System.Collections.Generic;
using Unisave.Entities;

public class GameDataEntity : Entity
{
    public EntityReference<UserEntity> userEntity;

    public int finalScore;
    public List<MissionResult> missionResults;
    public List<QuizResult> quizResults;
    public List<RoueResult> roueResult;
    public float minutesPlayed;
}

public class MissionResult
{
    public string missionName;
    public List<string> choices;
}

public class QuizResult
{
    public int answerIndex;
}

public class RoueResult
{
    public int roueIndex;
}
