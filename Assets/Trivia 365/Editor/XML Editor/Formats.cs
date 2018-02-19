public enum DifficultyLevel
{
    Easy,
    Medium,
    Hard,
    Bonus
}

[System.Serializable]
public class Format1
{
    public string Question;
    public string Image;
    public string CorrectAnswer;
    public string WrongAnswerA;
    public string WrongAnswerB;
    public string WrongAnswerC;

    public Format1()
    {
        this.Question =
            this.Image =
            this.CorrectAnswer =
            this.WrongAnswerA =
            this.WrongAnswerB =
            this.WrongAnswerC = string.Empty;
    }
}

[System.Serializable]
public class Format2
{
    public string Question;
    public bool IsTrue;
    public DifficultyLevel _Difficulty;
    public string Fact;

    public Format2()
	{
		this.Question =
        this.Fact = string.Empty;
		this.IsTrue = false;
		this._Difficulty = DifficultyLevel.Easy;
	}
}

[System.Serializable]
public class Format3
{
    public string Question;
    public string Image;
    public bool IsTrue;
    public string Fact;
    public string CorrectAnswer;
    public string WrongAnswerA;
    public string WrongAnswerB;
    public string WrongAnswerC;

    public Format3()
    {
		this.Question =
			this.Image =
            this.Fact =
            this.CorrectAnswer =
            this.WrongAnswerA =
            this.WrongAnswerB =
            this.WrongAnswerC = string.Empty;
       		this.IsTrue = false;
    }
}