using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC_MemoryCard_Reader.MemoryCard
{
    /// <summary>
    /// This class represents a single entry in the MemoryCard directory, and contains all the information
    /// relevant to reading out the stored SaveGame data from the MemoryCard.
    /// The class has simple properties to represent the information stored in the Directory.
    /// </summary>
    internal class DirectoryEntry
    {
        public string GameCode { get; internal set; }
        public short MakerCode { get; internal set; }
        public BannerGfxFormat Banner {  get; internal set; }
        public string FileName { get; internal set; } = "";
        public int LastModification { get; internal set; }
        public int ImageDataOffset { get;internal set; }
        public ICollection<IconGfxFormat> Icons { get; internal set; } = new List<IconGfxFormat>(8);
        public short AnimationSpeed { get; internal set; }
        public byte FilePermissions { get; internal set; }
        public byte CopyCounter { get; internal set; }
        public short BlockNumber { get; internal set; }
        public short BlockCount { get; internal set;}
        public int CommentAddress { get; internal set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"\tGameCode: {GameCode}");
            sb.AppendLine($"\tMakerCode: {MakerCode}");
            sb.AppendLine($"\tBanner: {Banner.ToString()}");
            sb.AppendLine($"\tFileName: {FileName}");
            sb.AppendLine($"\tLastModification: {LastModification}");
            sb.AppendLine($"\tImageDataOffset: {ImageDataOffset}");
            sb.AppendLine($"\tIcons: {Icons.Count} detected");
            foreach (IconGfxFormat icon in Icons)
                sb.AppendLine($"\t\t{icon.Type}");
            sb.AppendLine($"\tAnimationSpeed: {AnimationSpeed}");
            sb.AppendLine($"\tFilePermissions: {Convert.ToString(FilePermissions).PadLeft(8, '0')}");
            sb.AppendLine($"\tCopyCounter: {CopyCounter}");
            sb.AppendLine($"\tBlockNumber: {BlockNumber}");
            sb.AppendLine($"\tBlockCount: {BlockCount}");
            sb.AppendLine($"\tCommentAddress: {CommentAddress}");

            return sb.ToString();
        }
    }
}
