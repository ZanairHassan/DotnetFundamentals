using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace UtilityLibrary
{
    public static class Loggings
    {
        public static void MessageLog(string message)
        {
            {
                string projectPath = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;

                string folderPath = Path.Combine(projectPath, "Loggings");
                string filePath = Path.Combine(folderPath, "MessageLog.txt");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("-----------------------------------------------");
                sb.AppendLine(DateTime.Now.ToString());
                sb.AppendLine(message);
                sb.AppendLine("-----------------------------------------------");
                File.AppendAllText(filePath, sb.ToString());
            }
        }
    }
}
