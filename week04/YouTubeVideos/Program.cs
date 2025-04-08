class Program
{
    static void Main(string[] args)
    {
        // Create videos
        Video video1 = new Video("C# Tutorial for Beginners", "Programming with Mosh", 3600);
        Video video2 = new Video("Learn Python in 1 Hour", "Tech With Tim", 1800);
        Video video3 = new Video("Web Development Full Course", "freeCodeCamp", 10800);
        Video video4 = new Video("Machine Learning Basics", "Sentdex", 5400);

        // Add comments to video1
        video1.AddComment(new Comment("John Doe", "Great tutorial!"));
        video1.AddComment(new Comment("Jane Smith", "Very helpful for beginners."));
        video1.AddComment(new Comment("Mike Johnson", "Could you make one about OOP?"));

        // Add comments to video2
        video2.AddComment(new Comment("PythonLover", "Python is awesome!"));
        video2.AddComment(new Comment("CodeNewbie", "I learned so much in just 30 minutes."));
        video2.AddComment(new Comment("DevGuru", "Clear and concise explanation."));

        // Add comments to video3
        video3.AddComment(new Comment("WebDevStudent", "This is exactly what I needed!"));
        video3.AddComment(new Comment("FutureDeveloper", "12 hours well spent."));
        video3.AddComment(new Comment("Designer123", "The CSS section was particularly helpful."));
        video3.AddComment(new Comment("CodeMaster", "freeCodeCamp never disappoints."));

        // Add comments to video4
        video4.AddComment(new Comment("AIBeginner", "Finally understand neural networks!"));
        video4.AddComment(new Comment("DataScientist", "Good introduction to ML concepts."));
        video4.AddComment(new Comment("MathWizard", "The math explanations were clear."));

        // Put videos in a list
        List<Video> videos = new List<Video> { video1, video2, video3, video4 };

        // Display video information and comments
        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.GetTitle()}");
            Console.WriteLine($"Author: {video.GetAuthor()}");
            Console.WriteLine($"Length: {video.GetLength()} seconds");
            Console.WriteLine($"Number of comments: {video.GetNumberOfComments()}");
            
            Console.WriteLine("Comments:");
            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"- {comment.GetCommenter()}: {comment.GetText()}");
            }
            
            Console.WriteLine(); // Add a blank line between videos
        }
    }
}