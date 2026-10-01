using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class Exobeam : ModProjectile, ILocalizedModType, IModType
{
	public int TargetIndex = -1;

	public static float MaxWidth = 30f;

	public static Asset<Texture2D> BloomTex;

	public static Asset<Texture2D> SlashTex;

	public static Asset<Texture2D> TrailTex;

	public new string LocalizationCategory => "Projectiles.Melee";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 30;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 360;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = base.Projectile.MaxUpdates * 12;
	}

	public override void AI()
	{
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		if (Time >= (float)Exoblade.BeamNoHomeTime)
		{
			if (TargetIndex >= 0)
			{
				if (!Main.npc[TargetIndex].active || !Main.npc[TargetIndex].CanBeChasedBy())
				{
					TargetIndex = -1;
				}
				else
				{
					Vector2 idealVelocity = base.Projectile.SafeDirectionTo(Main.npc[TargetIndex].Center) * (((Vector2)(ref base.Projectile.velocity)).Length() + 6.5f);
					base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, idealVelocity, 0.08f);
				}
			}
			if (TargetIndex == -1)
			{
				NPC potentialTarget = base.Projectile.Center.ClosestNPCAt(1600f, ignoreTiles: false);
				if (potentialTarget != null)
				{
					TargetIndex = potentialTarget.whoAmI;
				}
				else
				{
					Projectile projectile = base.Projectile;
					projectile.velocity *= 0.99f;
				}
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (Main.rand.NextBool())
		{
			Color dustColor = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.9f);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(20f, 20f) + base.Projectile.velocity, 267, base.Projectile.velocity * -2.6f, 0, dustColor);
			dust.scale = 0.3f;
			dust.fadeIn = Main.rand.NextFloat() * 1.2f;
			dust.noGravity = true;
		}
		base.Projectile.scale = Utils.GetLerpValue(0f, 0.1f, (float)base.Projectile.timeLeft / 600f, clamped: true);
		if (base.Projectile.FinalExtraUpdate())
		{
			Time++;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in Exoblade.BeamHitSound, target.Center);
		if (Main.myPlayer == base.Projectile.owner)
		{
			int slash = Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), target.Center, base.Projectile.velocity * 0.1f, ModContent.ProjectileType<ExobeamSlashCreator>(), base.Projectile.damage, 0f, base.Projectile.owner, target.whoAmI, base.Projectile.velocity.ToRotation());
			if (Main.projectile.IndexInRange(slash))
			{
				Main.projectile[slash].timeLeft = 20;
			}
		}
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		Color white = Color.White;
		((Color)(ref white)).A = 0;
		return white * base.Projectile.Opacity;
	}

	public float TrailWidth(float completionRatio, Vector2 vertexPos)
	{
		return Utils.GetLerpValue(1f, 0.4f, completionRatio, clamped: true) * (float)Math.Sin(Math.Acos(1f - Utils.GetLerpValue(0f, 0.15f, completionRatio, clamped: true))) * Utils.GetLerpValue(0f, 0.1f, (float)base.Projectile.timeLeft / 600f, clamped: true) * MaxWidth;
	}

	public Color TrailColor(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(Color.Cyan, new Color(0, 0, 255), completionRatio);
	}

	public float MiniTrailWidth(float completionRatio, Vector2 vertexPos)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return TrailWidth(completionRatio, vertexPos) * 0.8f;
	}

	public Color MiniTrailColor(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.White;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0550: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_059a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f8: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 595)
		{
			return false;
		}
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		float bladeScale = Utils.GetLerpValue(3f, 13f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true) * 1.2f;
		Vector2 position = base.Projectile.oldPos[2] + base.Projectile.Size / 2f - Main.screenPosition;
		Color color = Color.White;
		((Color)(ref color)).A = 0;
		Main.EntitySpriteDraw(texture, position, null, color, base.Projectile.rotation + (float)Math.PI / 4f, texture.Size() / 2f, bladeScale * base.Projectile.scale, (SpriteEffects)0);
		if (BloomTex == null)
		{
			BloomTex = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
		}
		Texture2D bloomTex = BloomTex.Value;
		Color mainColor = CalamityUtils.MulticolorLerp((Main.GlobalTimeWrappedHourly * 0.5f + (float)base.Projectile.whoAmI * 0.12f) % 1f, Color.Cyan, Color.Lime, Color.GreenYellow, Color.Goldenrod, Color.Orange);
		Color secondaryColor = CalamityUtils.MulticolorLerp((Main.GlobalTimeWrappedHourly * 0.5f + (float)base.Projectile.whoAmI * 0.12f + 0.2f) % 1f, Color.Cyan, Color.Lime, Color.GreenYellow, Color.Goldenrod, Color.Orange);
		Vector2 position2 = base.Projectile.oldPos[2] + base.Projectile.Size / 2f - Main.screenPosition;
		color = mainColor * 0.1f;
		((Color)(ref color)).A = 0;
		Main.EntitySpriteDraw(bloomTex, position2, null, color, 0f, bloomTex.Size() / 2f, 1.3f * base.Projectile.scale, (SpriteEffects)0);
		Vector2 position3 = base.Projectile.oldPos[1] + base.Projectile.Size / 2f - Main.screenPosition;
		color = mainColor * 0.5f;
		((Color)(ref color)).A = 0;
		Main.EntitySpriteDraw(bloomTex, position3, null, color, 0f, bloomTex.Size() / 2f, 0.34f * base.Projectile.scale, (SpriteEffects)0);
		Main.spriteBatch.EnterShaderRegion();
		if (TrailTex == null)
		{
			TrailTex = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/BasicTrail", (AssetRequestMode)2);
		}
		GameShaders.Misc["CalamityMod:ExobladePierce"].SetShaderTexture(TrailTex);
		GameShaders.Misc["CalamityMod:ExobladePierce"].UseImage2("Images/Extra_189");
		GameShaders.Misc["CalamityMod:ExobladePierce"].UseColor(mainColor);
		GameShaders.Misc["CalamityMod:ExobladePierce"].UseSecondaryColor(secondaryColor);
		GameShaders.Misc["CalamityMod:ExobladePierce"].Apply();
		GameShaders.Misc["CalamityMod:ExobladePierce"].Apply();
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(TrailWidth, TrailColor, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ExobladePierce"]), 30);
		GameShaders.Misc["CalamityMod:ExobladePierce"].UseColor(Color.White);
		GameShaders.Misc["CalamityMod:ExobladePierce"].UseSecondaryColor(Color.White);
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(MiniTrailWidth, MiniTrailColor, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ExobladePierce"]), 30);
		Main.spriteBatch.ExitShaderRegion();
		Vector2 position4 = base.Projectile.oldPos[2] + base.Projectile.Size / 2f - Main.screenPosition;
		color = Color.White * 0.2f;
		((Color)(ref color)).A = 0;
		Main.EntitySpriteDraw(bloomTex, position4, null, color, 0f, bloomTex.Size() / 2f, 0.78f * base.Projectile.scale, (SpriteEffects)0);
		Vector2 position5 = base.Projectile.oldPos[1] + base.Projectile.Size / 2f - Main.screenPosition;
		color = Color.White * 0.5f;
		((Color)(ref color)).A = 0;
		Main.EntitySpriteDraw(bloomTex, position5, null, color, 0f, bloomTex.Size() / 2f, 0.2f * base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
