using System.Drawing.Imaging;

namespace WindowsFormsApp
{
    public class PlayerImageManager
    {
        private const string IMAGES_DIR = "Resources/PlayerImages";
        private const string DEFAULT_IMAGE_PATH = "Resources/PlayerImages/default_player.png";
        private static Image defaultImage;

        public PlayerImageManager()
        {
            EnsureDirectoryExists();
            LoadDefaultImage();
        }

        private void EnsureDirectoryExists()
        {
            if (!Directory.Exists(IMAGES_DIR))
            {
                Directory.CreateDirectory(IMAGES_DIR);
            }
        }

        private void LoadDefaultImage()
        {
            try
            {
                if (defaultImage == null)
                {
                    using (var stream = new FileStream(DEFAULT_IMAGE_PATH, FileMode.Open, FileAccess.Read))
                    {
                        defaultImage = new Bitmap(Image.FromStream(stream));
                    }
                }
            }
            catch (Exception)
            {
                using (var stream = new FileStream(DEFAULT_IMAGE_PATH, FileMode.Open, FileAccess.Read))
                {
                    defaultImage = new Bitmap(Image.FromStream(stream));
                }
            }
        }

        public Image LoadPlayerImage(string playerName)
        {
            try
            {
                string imagePath = GetImagePath(playerName);
                if (File.Exists(imagePath))
                {
                    using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        return new Bitmap(Image.FromStream(stream));
                    }
                }
                return new Bitmap(defaultImage);
            }
            catch (Exception)
            {
                return new Bitmap(defaultImage);
            }
        }

        public async Task<string> SavePlayerImageAsync(string playerName, Image image)
        {
            try
            {
                string imagePath = GetImagePath(playerName);
                using (var stream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                {

                    using (var bitmap = new Bitmap(image))
                    {
                        await Task.Run(() => bitmap.Save(stream, ImageFormat.Png));
                    }
                }
                return imagePath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to save image: {ex.Message}");
            }
        }

        private string GetImagePath(string playerName)
        {
            string sanitizedName = string.Join("_", playerName.Split(Path.GetInvalidFileNameChars()));
            return Path.Combine(IMAGES_DIR, $"{sanitizedName}.png");
        }
    }
}  