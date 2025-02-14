using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Domain.Users;
using Rommelmarkten.Api.Domain.ValueObjects;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rommelmarkten.Api.Infrastructure.Services
{
    public class AvatarGenerator : IAvatarGenerator
    {
        private List<string> backgroundColors = new List<string> { "3C79B2", "FF8F88", "6FB9FF", "C0CC44", "AFB28C", "A18CB2" };
        private string foregroundColor = "#F5F5F5"; //whitesmoke

        public Task<Blob> GenerateAvatar(IUser user, int size = 128)
        {
            return Task.Run(() =>
            {
                string avatarText = user.UserName.First().ToString().ToUpper();
                if (!string.IsNullOrWhiteSpace($"{user.FirstName}{user.LastName}"))
                {
                    avatarText = $"{user.FirstName.FirstOrDefault()}{user.LastName.FirstOrDefault()}".ToUpper();
                }
                var randomIndex = new Random().Next(0, backgroundColors.Count - 1);
                var backgroundColor = "#" + backgroundColors[randomIndex];

                var bitmap = new SKBitmap(size, size,
                             SKImageInfo.PlatformColorType,
                             SKAlphaType.Premul);
                var canvas = new SKCanvas(bitmap);
                canvas.Clear(SKColor.Parse(backgroundColor));

                var midy = size / 2; //canvas.LocalClipBounds.Size.ToSizeI().Height / 2;
                var midx = size / 2; //canvas.LocalClipBounds.Size.ToSizeI().Width / 2;

                var family = SKTypeface.FromFamilyName("Arial",
                     SKFontStyleWeight.Normal, SKFontStyleWidth.Normal,
                     SKFontStyleSlant.Upright);

                var font = new SKFont(family, midx / 1.15f);

                var textSize = midx / 1.15f;

                var paint = new SKPaint
                {
                    IsAntialias = true,
                    Style = SKPaintStyle.Fill,
                    Color = SKColor.Parse(foregroundColor)
                };
                var rect = new SKRect();
                
                canvas.DrawText(avatarText, midx, midy - rect.MidY, SKTextAlign.Center, font, paint);

                var skImage = SKImage.FromBitmap(bitmap);

                var content = (skImage.Encode(SKEncodedImageFormat.Png, 100)).AsSpan().ToArray();
                var blob = new Blob($"{user.Id}.png", "image/png", content);
                return blob;
            });
        }
    }
}
