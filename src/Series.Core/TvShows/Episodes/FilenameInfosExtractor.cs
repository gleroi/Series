using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Series.Core.TvShows.Episodes
{
    /// <summary>
    /// Try to extract episode number and season from filename
    /// </summary>
    public class FilenameInfosExtractor
    {
        private string[] _patterns = new string[] {
            @"saison_(\d+)_episode_(\d+)\-(vf|vostfr)",
            @"s?(\d+)[epx]{1,2}(\d+)",
            @"(\d)(\d\d)"
        };

        private IEnumerable<string> Patterns { get { return _patterns; } }

        public Episode Extract(string filename)
        {
            Episode ep = new Episode();
            foreach (string pattern in Patterns)
            {
                Regex rex = new Regex(pattern);
                if (rex.IsMatch(filename))
                {
                    Match match = rex.Match(filename);

                    bool isValid = true;
                    int result = -1;
                    string season = match.Groups[1].Value;
                    isValid = isValid && int.TryParse(season, out result);
                    if (isValid)
                        ep.Season = result;

                    string episode = match.Groups[2].Value;
                    isValid = isValid && int.TryParse(episode, out result);
                    if (isValid)
                        ep.Opus = result;

                    if (isValid)
                        return ep;
                }
            }
            return null;
        }
    }
}