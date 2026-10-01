using System;
using System.IO;
using System.Linq;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Graphics.Primitives;
using CalamityMod.NPCs.ExoMechs.Artemis;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class ArtemisSpinLaserbeam : BaseLaserbeamProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public int OwnerIndex
	{
		get
		{
			return (int)base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public override float MaxScale => 1f;

	public override float MaxLaserLength => 4800f;

	public override float Lifetime => 600f;

	public override Color LaserOverlayColor
	{
		get
		{
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			return new Color(250, 180, 100, 100);
		}
	}

	public override Color LightCastColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.White;
		}
	}

	public override Texture2D LaserBeginTexture => TextureAssets.Projectile[base.Type].Value;

	public override Texture2D LaserMiddleTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/AresLaserBeamMiddle", (AssetRequestMode)1).Value;

	public override Texture2D LaserEndTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/AresLaserBeamEnd", (AssetRequestMode)1).Value;

	public override string Texture => "CalamityMod/Projectiles/Boss/AresLaserBeamStart";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.hostile = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 600;
		base.CooldownSlot = 1;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(base.Projectile.localAI[1]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
		base.Projectile.localAI[1] = reader.ReadSingle();
	}

	public override void AttachToSomething()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		if (Main.npc[OwnerIndex].active && Main.npc[OwnerIndex].type == ModContent.NPCType<Artemis>())
		{
			Vector2 fireFrom = Main.npc[OwnerIndex].Center + Vector2.UnitY * Main.npc[OwnerIndex].gfxOffY;
			fireFrom += base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 50f;
			base.Projectile.Center = fireFrom;
			if (Main.npc[OwnerIndex].Calamity().newAI[0] != 3f)
			{
				base.Projectile.Kill();
			}
		}
		else
		{
			base.Projectile.Kill();
		}
	}

	public override float DetermineLaserLength()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		float[] sampledLengths = new float[10];
		Collision.LaserScan(base.Projectile.Center, base.Projectile.velocity, (float)base.Projectile.width * base.Projectile.scale, MaxLaserLength, sampledLengths);
		float newLaserLength = sampledLengths.Average();
		if (!Collision.CanHitLine(Main.npc[OwnerIndex].Center, 1, 1, Main.player[Main.npc[OwnerIndex].target].Center, 1, 1))
		{
			newLaserLength = MaxLaserLength;
		}
		return newLaserLength;
	}

	public override void UpdateLaserMotion()
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = Main.npc[OwnerIndex].rotation;
		base.Projectile.velocity = (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2();
	}

	public override void PostAI()
	{
		base.Projectile.frameCounter++;
		if ((float)base.Projectile.frameCounter % 5f == 0f)
		{
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
		}
	}

	public float LaserWidthFunction(float _, Vector2 vertexPos)
	{
		return base.Projectile.scale * (float)base.Projectile.width;
	}

	public static Color LaserColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		float colorInterpolant = (float)Math.Sin(Main.GlobalTimeWrappedHourly * -3.2f + completionRatio * 23f) * 0.5f + 0.5f;
		return Color.Lerp(Color.Orange, Color.Red, colorInterpolant * 0.67f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity == Vector2.Zero)
		{
			return false;
		}
		Vector2 laserEnd = base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * base.LaserLength;
		Vector2[] baseDrawPoints = (Vector2[])(object)new Vector2[8];
		for (int i = 0; i < baseDrawPoints.Length; i++)
		{
			baseDrawPoints[i] = Vector2.Lerp(base.Projectile.Center, laserEnd, (float)i / ((float)baseDrawPoints.Length - 1f));
		}
		GameShaders.Misc["CalamityMod:ArtemisLaser"].UseColor(Color.Cyan);
		GameShaders.Misc["CalamityMod:ArtemisLaser"].UseImage1("Images/Extra_189");
		GameShaders.Misc["CalamityMod:ArtemisLaser"].UseImage2("Images/Misc/Perlin");
		PrimitiveRenderer.RenderTrail(baseDrawPoints, new PrimitiveSettings(LaserWidthFunction, LaserColorFunction, null, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ArtemisLaser"]), 64);
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
		}
	}

	public override bool CanHitPlayer(Player target)
	{
		return base.Projectile.scale >= 0.5f;
	}
}
