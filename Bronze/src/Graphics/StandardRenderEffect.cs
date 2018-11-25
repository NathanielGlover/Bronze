namespace Bronze.Graphics
{
    public class StandardRenderEffect : FullRenderEffect
    {
        private const string VertFile = "/Users/nathanielglover/Bronze/Bronze/res/standard.vert";
        private const string FragFile = "/Users/nathanielglover/Bronze/Bronze/res/standard.frag";

        public StandardRenderEffect() : base(ResourceLoader.LoadShader(VertFile, FragFile)) { }
    }
}