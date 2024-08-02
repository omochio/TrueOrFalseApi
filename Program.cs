using TrueOrFalseApi.Properties;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/highscore", GetHighscore);
app.MapPut("/highscore", UpdateHighscore);

app.Run();

static async Task<IResult> GetHighscore()
{
    var resdir = Environment.GetEnvironmentVariable("RES_DIR");
    var highscorePath = resdir + "/highscore.txt";

    if (File.Exists(highscorePath))
    {
        var highscore = int.Parse(await File.ReadAllTextAsync(highscorePath));
        return Results.Ok(highscore);
    }
    else
    {
        return Results.Ok(0);
    }
}

static async Task<IResult> UpdateHighscore(Score score)
{
    var resdir = Environment.GetEnvironmentVariable("RES_DIR");
    var highscorePath = resdir + "/highscore.txt";

    if (File.Exists(highscorePath))
    {
        if (score.Value > int.Parse(await File.ReadAllTextAsync(highscorePath)))
        {
            await File.WriteAllTextAsync(highscorePath, score.Value.ToString());
        }
        return Results.Ok();
    }
    else
    {
        File.CreateText(highscorePath);
        await File.WriteAllTextAsync(highscorePath, score.Value.ToString());
        return Results.Ok();
    }

}