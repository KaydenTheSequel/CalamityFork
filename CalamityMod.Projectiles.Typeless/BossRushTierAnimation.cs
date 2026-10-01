using CalamityMod.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class BossRushTierAnimation : ModProjectile, ILocalizedModType, IModType
{
	public const int FrameChangeRate = 4;

	public const int TotalFrames = 41;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public int Tier => (int)base.Projectile.ai[0];

	public Player Owner => Main.player[base.Projectile.owner];

	public override string Texture => "CalamityMod/Projectiles/Typeless/BossRushTier1Animation";

	public override void SetDefaults()
	{
		base.Projectile.width = 62;
		base.Projectile.height = 64;
		base.Projectile.aiStyle = -1;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 164;
		base.Projectile.penetrate = -1;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Bottom = Owner.Top - Vector2.UnitY * base.Projectile.scale * 36f;
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 4;
		if (base.Projectile.frame >= 41)
		{
			base.Projectile.frame = 41;
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			float volume = 2.8f;
			switch (Tier)
			{
			case 1:
			{
				SoundStyle style = BossRushEvent.Tier2TransitionSound with
				{
					Volume = volume
				};
				SoundEngine.PlaySound(in style, Main.LocalPlayer.Center);
				break;
			}
			case 2:
			{
				SoundStyle style = BossRushEvent.Tier2TransitionSound with
				{
					Volume = volume
				};
				SoundEngine.PlaySound(in style, Main.LocalPlayer.Center);
				break;
			}
			case 3:
			{
				SoundStyle style = BossRushEvent.Tier3TransitionSound with
				{
					Volume = volume
				};
				SoundEngine.PlaySound(in style, Main.LocalPlayer.Center);
				break;
			}
			case 4:
			{
				SoundStyle style = BossRushEvent.Tier4TransitionSound with
				{
					Volume = volume
				};
				SoundEngine.PlaySound(in style, Main.LocalPlayer.Center);
				break;
			}
			case 5:
			{
				SoundStyle style = BossRushEvent.Tier5TransitionSound with
				{
					Volume = volume
				};
				SoundEngine.PlaySound(in style, Main.LocalPlayer.Center);
				break;
			}
			}
			base.Projectile.localAI[0] = 1f;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return Color.White * base.Projectile.Opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>($"CalamityMod/Projectiles/Typeless/BossRushTier{Tier}Animation", (AssetRequestMode)2).Value;
		Rectangle frame = texture.Frame(41, 1, base.Projectile.frame % 41, base.Projectile.frame / 41);
		Vector2 origin = frame.Size() * 0.5f;
		Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition, (Rectangle?)frame, base.Projectile.GetAlpha(lightColor), 0f, origin, base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}
}
