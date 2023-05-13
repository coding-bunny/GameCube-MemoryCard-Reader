using System;
using System.Collections.Generic;
using System.Linq;
using System.Buffers.Binary;
using System.Text;
using System.Threading.Tasks;
using GC_MemoryCard_Reader.MemoryCard;

namespace GC_MemoryCard_Reader.Parsers
{
    /// <summary>
    /// This class contains the functionality to parse the contents of a memory card dump stored in a <c>.raw</c> file.
    /// </summary>
    internal class RawParser
    {
        string _path;

        /// <summary>
        /// Creates a new instance of the RawParser and opens a FileStream to the provided path.
        /// </summary>
        /// <param name="path"></param>
        public RawParser(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"Could not locate the file specified at {path}");
             
            _path = path;
        }

        /// <summary>
        /// Parses the binary file and extracts the entire header from the contents.
        /// The header of the MemoryCard can always be found in the address range <c>0x0000</c> - <c>0x2000</c>.
        /// If we simply read those bytes from the binary file, we can parse the data in memory and return a Header object.
        /// </summary>
        /// <returns></returns>
        public MemoryCard.Header ExtractHeader()
        {
            // Open the file, and read the first 0x2000 bytes, which is the header we care about.
            using (var stream = File.Open(_path, FileMode.Open))
            {
                using(var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {
                    byte[] headerRaw = reader.ReadBytes(0x2000);

                    return new MemoryCard.Header()
                    {
                        TimeSpan = BinaryPrimitives.ReadUInt64BigEndian(headerRaw.AsSpan<byte>(0x000C, 8)),
                        CardId = Encoding.UTF8.GetString(headerRaw.AsSpan<byte>(0x0014, 6).ToArray()),
                        Size = BinaryPrimitives.ReadUInt16BigEndian(headerRaw.AsSpan<byte>(0x0022, 2)),
                        Encoding = BinaryPrimitives.ReadUInt16BigEndian(headerRaw.AsSpan<byte>(0x0024, 2)),
                        UpdateCounter = BinaryPrimitives.ReadUInt16BigEndian(headerRaw.AsSpan<byte>(0x01FA, 2)),
                        CheckSumOne = BinaryPrimitives.ReadUInt16BigEndian(headerRaw.AsSpan<byte>(0x01FC, 2)),
                        CheckSumTwo = BinaryPrimitives.ReadUInt16BigEndian(headerRaw.AsSpan<byte>(0x01FE, 2))
                    };
                }
            }
        }

        public MemoryCard.Directory ExtractDirectory(MemoryCard.DirectoryType type = MemoryCard.DirectoryType.Standard)
        {
            // Open the file, and read the first 0x2000 bytes, which is the header we care about.
            using (var stream = File.Open(_path, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {
                    // Move the underlying stream to the correct position and read out the desired block of the Directory.
                    // We know the Directory block is 0x2000 bytes long, regardless if it's the backup or not.
                    reader.BaseStream.Seek((long)type, SeekOrigin.Begin);
                    byte[] directoryRaw = reader.ReadBytes(0x2000);
                    var directory = new MemoryCard.Directory();

                    // Parse all 127 entries.
                    for(var i = 0; i < 127; i++)
                    {
                        var entryOffset = i * 0x40;

                        uint gameCode = BinaryPrimitives.ReadUInt32BigEndian(directoryRaw.AsSpan<byte>(entryOffset + 0x00, 0x04));

                        if (gameCode == uint.MaxValue)
                            break;

                        directory.Entries.Add(new MemoryCard.DirectoryEntry()
                        {
                            GameCode = gameCode,
                            MakerCode = BinaryPrimitives.ReadUInt16BigEndian(directoryRaw.AsSpan<byte>(entryOffset + 0x04, 0x02)),
                            Banner = (BannerGfxFormat)directoryRaw.AsSpan<byte>(entryOffset + 0x07, 0x01)[0],
                            FileName = Encoding.ASCII.GetString(directoryRaw.AsSpan<byte>(entryOffset + 0x08, 0x020)),
                            LastModification = BinaryPrimitives.ReadUInt32BigEndian(directoryRaw.AsSpan<byte>(entryOffset + 0x28, 0x04)),
                            ImageDataOffset = BinaryPrimitives.ReadUInt32BigEndian(directoryRaw.AsSpan<byte>(entryOffset + 0x2C, 0x04)),
                            IconFormat = BinaryPrimitives.ReadUInt16BigEndian(directoryRaw.AsSpan<byte>(entryOffset + 0x30, 0x02)),
                            AnimationSpeed = BinaryPrimitives.ReadUInt16BigEndian(directoryRaw.AsSpan<byte>(entryOffset + 0x32, 0x02)),
                            FilePermissions = directoryRaw.AsSpan<byte>(entryOffset + 0x34, 0x01)[0],
                            CopyCounter = directoryRaw.AsSpan<byte>(entryOffset + 0x35, 0x01)[0],
                            BlockNumber = BinaryPrimitives.ReadUInt16BigEndian(directoryRaw.AsSpan<byte>(entryOffset + 0x36, 0x02)),
                            BlockCount = BinaryPrimitives.ReadUInt16BigEndian(directoryRaw.AsSpan<byte>(entryOffset + 0x38, 0x02)),
                            CommentAddress = BinaryPrimitives.ReadUInt32BigEndian(directoryRaw.AsSpan<byte>(entryOffset + 0x3c, 0x04)),
                        });
                    }                    

                    return directory;
                }
            }
        }
    }
}
