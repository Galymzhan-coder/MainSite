namespace Administration.Configs.Image
{
    public class ImageConfig
    {
        public BlogConfig Blogs { get; set; }

    }
    public class BlogConfig
    {
        public int[][] Image { get; set; }
    }
}
