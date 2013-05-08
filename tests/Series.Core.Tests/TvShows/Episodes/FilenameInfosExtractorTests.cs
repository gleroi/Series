using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Series.Core.TvShows;
using Series.Core.TvShows.Episodes;
using Xunit;

namespace Series.Core.Tests.TvShows.Episodes
{
    public class FilenameInfosExtractorTests
    {
        private const int expectedEpisode = 4;
        private const int expectedSeason = 3;
        private FilenameInfosExtractor extractor = new FilenameInfosExtractor();

        [Fact]
        public void ShouldFind_sXeX()
        {
            Test("Californication.s3e4.Je.Vous.Hais.Tous.FR.DVDRip.XviD-BaLLanTeAm.avi");
        }

        [Fact]
        public void ShouldFind_sXeXX()
        {
            Test("Californication.s3e04.Je.Vous.Hais.Tous.FR.DVDRip.XviD-BaLLanTeAm.avi");
        }

        [Fact]
        public void ShouldFind_sXXepXX()
        {
            Test("Californication.s03ep04.Je.Vous.Hais.Tous.FR.DVDRip.XviD-BaLLanTeAm.avi");
        }

        [Fact]
        public void ShouldFind_sXXeXX()
        {
            Test("Californication.s03e04.Je.Vous.Hais.Tous.FR.DVDRip.XviD-BaLLanTeAm.avi");
        }

        [Fact]
        public void ShouldFind_SXXEXX()
        {
            Test("Californication.S03E04.Je.Vous.Hais.Tous.FR.DVDRip.XviD-BaLLanTeAm.avi");
        }

        [Fact]
        public void ShouldFind_sXXxXX()
        {
            Test("Californication.s03x04.Je.Vous.Hais.Tous.FR.DVDRip.XviD-BaLLanTeAm.avi");
        }

        [Fact]
        public void ShouldFind_XeX()
        {
            Test("Californication.3e4.Je.Vous.Hais.Tous.FR.DVDRip.XviD-BaLLanTeAm.avi");
        }

        [Fact]
        public void ShouldFind_XeXX()
        {
            Test("Californication.3e04.Je.Vous.Hais.Tous.FR.DVDRip.XviD-BaLLanTeAm.avi");
        }

        [Fact]
        public void ShouldFind_XXeXX()
        {
            Test("Californication.03e04.Je.Vous.Hais.Tous.FR.DVDRip.XviD-BaLLanTeAm.avi");
        }

        [Fact]
        public void ShouldFind_XXX()
        {
            Test("Californication.304.Je.Vous.Hais.Tous.FR.DVDRip.XviD-BaLLanTeAm.avi");
        }

        private void Test(string filename)
        {
            Episode ep = extractor.Extract(filename);

            Assert.NotNull(ep);
            Assert.Equal(expectedSeason, ep.Season);
            Assert.Equal(expectedEpisode, ep.Opus);
        }
    }
}