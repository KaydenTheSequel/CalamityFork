using System;
using CalamityMod.Events;
using CalamityMod.Items.SummonItems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Events;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class TerminusHoldout : ModProjectile
{
	public SlotId ActivationSoundSlot;

	public const int Lifetime = 300;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Terminus>();

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Time => ref base.Projectile.ai[0];

	public override string Texture => "CalamityMod/Items/SummonItems/Terminus";

	public override void SetDefaults()
	{
		base.Projectile.width = 70;
		base.Projectile.height = 80;
		base.Projectile.aiStyle = -1;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 300;
		base.Projectile.penetrate = -1;
	}

	public override void AI()
	{
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		if (Owner.CantUseHoldout())
		{
			if (BossRushEvent.BossRushActive || BossRushEvent.StartTimer > 0)
			{
				BossRushEvent.SyncStartTimer(120);
			}
			if (SoundEngine.TryGetActiveSound(ActivationSoundSlot, out ActiveSound t) && t.IsPlaying)
			{
				t.Stop();
			}
			CreateMysticDeathDust();
			base.Projectile.Kill();
			return;
		}
		UpdatePlayerFields();
		if (BossRushEvent.BossRushActive || BossRushEvent.StartTimer > 0)
		{
			if (Time == 2f)
			{
				SoundEngine.PlaySound(in BossRushEvent.TerminusDeactivationSound, Main.LocalPlayer.Center);
			}
			float lifetime = Utils.GetLerpValue(0f, 30f, Time, clamped: true);
			if (Time % 5f == 4f)
			{
				BossRushEvent.SyncStartTimer((int)MathHelper.Lerp(0f, 120f, 1f - lifetime));
			}
			MoonlordDeathDrama.RequestLight(Utils.GetLerpValue(0f, 15f, Time, clamped: true), Main.LocalPlayer.Center);
			if (Time >= 45f)
			{
				BossRushEvent.End();
				base.Projectile.Kill();
			}
			return;
		}
		if (Time == 2f)
		{
			ActivationSoundSlot = SoundEngine.PlaySound(in BossRushEvent.TerminusActivationSound, Main.LocalPlayer.Center);
		}
		if (SoundEngine.TryGetActiveSound(ActivationSoundSlot, out ActiveSound t2) && t2.IsPlaying)
		{
			t2.Position = base.Projectile.Center;
		}
		if (base.Projectile.timeLeft == 1)
		{
			base.Projectile.Kill();
			CreateEffectsHandler();
			return;
		}
		if (base.Projectile.timeLeft >= 32)
		{
			CreateIdleMagicDust();
		}
		float currentShakePower = MathHelper.Lerp(0.2f, 8f, Utils.GetLerpValue(217.5f, 300f, Time, clamped: true));
		currentShakePower *= 1f - Utils.GetLerpValue(1000f, 3100f, Main.LocalPlayer.Distance(base.Projectile.Center), clamped: true);
		Main.LocalPlayer.SetScreenshake(currentShakePower);
	}

	public void CreateEffectsHandler()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in BossRushEvent.StartBuildupSound, Main.LocalPlayer.Center);
		Main.LocalPlayer.SetScreenshake(16f);
		if (Main.myPlayer == base.Projectile.owner)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BossRushEffectThing>(), 0, 0f, base.Projectile.owner);
		}
	}

	public void CreateMysticDeathDust()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(22f, 28f), 261);
				dust.velocity = -Vector2.UnitY * Main.rand.NextFloat(1.8f, 3.2f);
				dust.color = Color.White;
				dust.scale = Main.rand.NextFloat(1.1f, 1.35f);
				dust.fadeIn = 1.5f;
				dust.noGravity = true;
			}
		}
	}

	public void CreateIdleMagicDust()
	{
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			int dustCount = (int)Math.Round(MathHelper.SmoothStep(1f, 5f, Time / 300f));
			float outwardness = MathHelper.SmoothStep(40f, 150f, Time / 300f);
			float dustScale = MathHelper.Lerp(1.15f, 1.725f, Time / 300f);
			float colorTime = Time / 300f * Main.rand.NextFloat(0.65f, 1f);
			for (int i = 0; i < dustCount; i++)
			{
				Vector2 spawnPosition = base.Projectile.Center + Main.rand.NextVector2Unit() * outwardness * Main.rand.NextFloat(0.75f, 1.1f);
				Vector2 dustVelocity = (base.Projectile.Center - spawnPosition) * 0.085f + Owner.velocity;
				Dust dust = Dust.NewDustPerfect(spawnPosition, 264);
				dust.velocity = dustVelocity;
				dust.scale = dustScale * Main.rand.NextFloat(0.75f, 1.15f);
				dust.color = (Main.zenithWorld ? Color.Lerp(Color.LightCoral, Color.White, colorTime) : Color.Lerp(Color.Violet, Color.Goldenrod, colorTime));
				dust.noGravity = true;
				dust.noLight = true;
			}
		}
	}

	public void UpdatePlayerFields()
	{
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.spriteDirection = Owner.direction;
			base.Projectile.localAI[0] = 1f;
		}
		Owner.itemRotation = 0f;
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		Owner.ChangeDir(base.Projectile.spriteDirection);
		base.Projectile.Center = Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true) + Vector2.UnitX * (float)base.Projectile.spriteDirection * 26f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = (Main.zenithWorld ? ModContent.Request<Texture2D>("CalamityMod/Items/SummonItems/Terminus_GFB", (AssetRequestMode)2).Value : TextureAssets.Projectile[base.Type].Value);
		Vector2 baseDrawPosition = base.Projectile.Center - Main.screenPosition;
		Vector2 origin = texture.Size() * 0.5f;
		Color baseColor = Color.Lerp(base.Projectile.GetAlpha(lightColor), Color.White, Utils.GetLerpValue(40f, 120f, Time, clamped: true));
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection != 1);
		if (Time >= 150f)
		{
			float outwardness = MathHelper.SmoothStep(0f, 12f, Utils.GetLerpValue(150f, 270f, Time, clamped: true));
			Color afterimageColor = Color.Lerp(baseColor, Color.PaleGoldenrod, Utils.GetLerpValue(150f, 195f, Time, clamped: true)) * 0.225f;
			((Color)(ref afterimageColor)).A = 0;
			for (int i = 0; i < 10; i++)
			{
				Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 10f + Main.GlobalTimeWrappedHourly * 4.4f).ToRotationVector2() * outwardness;
				Main.EntitySpriteDraw(texture, baseDrawPosition + drawOffset, null, afterimageColor, 0f, origin, base.Projectile.scale, direction);
			}
		}
		Main.EntitySpriteDraw(texture, baseDrawPosition, null, baseColor, 0f, origin, base.Projectile.scale, direction);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		if (Main.zenithWorld)
		{
			return;
		}
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/SummonItems/TerminusGlow", (AssetRequestMode)2).Value;
		Vector2 baseDrawPosition = base.Projectile.Center - Main.screenPosition;
		Vector2 origin = texture.Size() * 0.5f;
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection != 1);
		if (Time >= 75f)
		{
			float outwardness = MathHelper.SmoothStep(0f, 4f, Utils.GetLerpValue(75f, 270f, Time, clamped: true));
			for (int i = 0; i < 3; i++)
			{
				Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 4f + Main.GlobalTimeWrappedHourly * 4.4f).ToRotationVector2() * outwardness;
				Main.EntitySpriteDraw(texture, baseDrawPosition + drawOffset, null, new Color(56, 12, 115, 0), 0f, origin, base.Projectile.scale, direction);
			}
		}
		Main.EntitySpriteDraw(texture, baseDrawPosition, null, Color.White, 0f, origin, base.Projectile.scale, direction);
	}
}
