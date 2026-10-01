using System;
using CalamityMod.Buffs.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class CosmilampMinion : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public int HoverOffsetIndex => (int)base.Projectile.ai[0];

	public float HoverOffsetInterpolant
	{
		get
		{
			float projectileCounts = Owner.ownedProjectileCounts[base.Type];
			if (projectileCounts <= 1f)
			{
				return 0.5f;
			}
			return (float)HoverOffsetIndex / (projectileCounts - 1f);
		}
	}

	public ref float Timer => ref base.Projectile.ai[1];

	public override string Texture => "CalamityMod/NPCs/Signus/CosmicLantern";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 20);
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = false;
		base.Projectile.tileCollide = false;
		base.Projectile.minionSlots = 2f;
		base.Projectile.timeLeft = 90000;
		base.Projectile.penetrate = -1;
		base.Projectile.minion = true;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		HandleMinionBools();
		DecideFrames();
		HoverInPlace();
		Timer++;
		NPC potentialTarget = base.Projectile.Center.MinionHoming(1360f, Owner);
		if (potentialTarget != null && (int)(Timer % 105f) == (int)(HoverOffsetInterpolant * 87f))
		{
			SoundEngine.PlaySound(in SoundID.Item158, base.Projectile.Center);
			if (Main.myPlayer == base.Projectile.owner)
			{
				Vector2 beamVelocity = base.Projectile.SafeDirectionTo(potentialTarget.Center).RotatedByRandom(0.3199999928474426) * Main.rand.NextFloat(9.4f, 11f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, beamVelocity, ModContent.ProjectileType<CosmilampBeam>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			}
		}
	}

	public void HandleMinionBools()
	{
		Owner.AddBuff(ModContent.BuffType<CosmilampBuff>(), 3600);
		if (base.Projectile.type == ModContent.ProjectileType<CosmilampMinion>())
		{
			if (Owner.dead)
			{
				Owner.Calamity().cLamp = false;
			}
			if (Owner.Calamity().cLamp)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	public void DecideFrames()
	{
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 8 % Main.projFrames[base.Type];
	}

	public void HoverInPlace()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		Vector2 hoverDestination = Owner.Top + Vector2.UnitY * Owner.gfxOffY + new Vector2(MathHelper.Lerp(-100f, 100f, HoverOffsetInterpolant), -80f);
		hoverDestination.Y += ((float)Math.Sin((float)Math.PI * 2f * HoverOffsetInterpolant + Timer / 50f) * 0.5f + 0.5f) * 40f;
		base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, hoverDestination, 0.02f).MoveTowards(hoverDestination, 8f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection == 1);
		Color val;
		for (int i = 0; i < base.Projectile.oldPos.Length; i++)
		{
			float afterimageFade = 1f - (float)i / (float)base.Projectile.oldPos.Length;
			val = Color.Lerp(Color.Fuchsia, Color.Cyan, afterimageFade);
			((Color)(ref val)).A = 25;
			Color afterimageDrawColor = val * base.Projectile.Opacity * afterimageFade * 0.6f;
			Vector2 afterimageDrawPosition = base.Projectile.oldPos[i] + base.Projectile.Size * 0.5f - Main.screenPosition;
			Main.EntitySpriteDraw(texture, afterimageDrawPosition, frame, afterimageDrawColor, base.Projectile.rotation, origin, base.Projectile.scale, direction);
		}
		for (int j = 0; j < 8; j++)
		{
			val = Color.Cyan;
			((Color)(ref val)).A = 25;
			Color afterimageDrawColor2 = val * base.Projectile.Opacity * 0.4f;
			Vector2 afterimageDrawPosition2 = base.Projectile.Center - Main.screenPosition + ((float)Math.PI * 2f * (float)j / 8f).ToRotationVector2() * 3f;
			Main.EntitySpriteDraw(texture, afterimageDrawPosition2, frame, afterimageDrawColor2, base.Projectile.rotation, origin, base.Projectile.scale, direction);
		}
		Main.EntitySpriteDraw(texture, drawPosition, frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, direction);
		return false;
	}
}
