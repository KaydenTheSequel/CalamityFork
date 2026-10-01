using System;
using System.Collections.Generic;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Graphics.Metaballs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class GalaxySmasherEcho : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle SlamHamSound = new SoundStyle("CalamityMod/Sounds/Item/GalaxySmasherSmash")
	{
		Volume = 0.7f
	};

	public static readonly SoundStyle Kunk = new SoundStyle("CalamityMod/Sounds/Item/TF2PanHit")
	{
		Volume = 1.1f
	};

	public float rotatehammer;

	public float speed;

	public NPC targeted;

	public Color usedColor;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/GalaxySmasher";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 15;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 86;
		base.Projectile.height = 72;
		base.Projectile.aiStyle = 0;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		List<Color> eColors = new List<Color>
		{
			Color.Aqua,
			Color.Magenta
		};
		float rate = Main.GlobalTimeWrappedHourly * 43f;
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		usedColor = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		rotatehammer += 2f;
		base.Projectile.rotation += MathHelper.ToRadians(rotatehammer) * (float)base.Projectile.direction;
		if (base.Projectile.timeLeft % 2 == 0)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -base.Projectile.velocity * 0.05f, "CalamityMod/Particles/SmallBloom", affectedByGravity: false, 13, 0.5f, usedColor, new Vector2(0.6f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.7f));
		}
		base.Projectile.extraUpdates = 7;
		if (base.Projectile.ai[1] != -5f)
		{
			targeted = Main.npc[(int)base.Projectile.ai[1]];
		}
		if (targeted == null || !targeted.CanBeChasedBy(base.Projectile) || !targeted.active)
		{
			targeted = base.Projectile.Center.ClosestNPCAt(2000f);
			base.Projectile.ai[1] = -5f;
		}
		if (targeted != null)
		{
			CalamityUtils.HomeInOnSelectedNPC(base.Projectile, targeted, ignoreTiles: true, 0.6f, 25f, 0.98f);
		}
		else
		{
			base.Projectile.Kill();
		}
		Vector2 offset = Utils.RotatedByRandom(new Vector2(12f, 0f), MathHelper.ToRadians(360f));
		Vector2 velOffset = Utils.RotatedBy(new Vector2(4f, 0f), (double)offset.ToRotation(), default(Vector2));
		Dust dust = Dust.NewDustPerfect(base.Projectile.Center + offset, 278, (-base.Projectile.velocity + velOffset) * Main.rand.NextFloat(0.3f, 1f), 0, default(Color), Main.rand.NextFloat(0.35f, 0.75f));
		dust.noGravity = true;
		dust.color = (Main.rand.NextBool() ? Color.Magenta : Color.Aqua);
		for (int i = 0; i < 2; i++)
		{
			GalaxyMetaball.SpawnParticle(base.Projectile.Center, -base.Projectile.velocity.RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(0.2f, 1f), 195f * Main.rand.NextFloat(0.9f, 1f));
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (targeted != null && target != targeted)
		{
			modifiers.SourceDamage *= 0.3f;
		}
		else
		{
			modifiers.SetCrit();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 420);
		if (target == targeted)
		{
			base.Projectile.Kill();
		}
	}

	public override bool PreKill(int timeLeft)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		Main.player[base.Projectile.owner].SetScreenshake(15f);
		if (Main.zenithWorld)
		{
			SoundEngine.PlaySound(in Kunk, base.Projectile.Center);
		}
		else
		{
			SoundEngine.PlaySound(in SlamHamSound, base.Projectile.Center);
		}
		for (int i = 0; i < 8; i++)
		{
			float rot = (float)Math.PI / 4f * (float)i;
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Aqua * 0.4f, "CalamityMod/Particles/HighResHollowCircleHardEdge", new Vector2(1.4f, 0.6f), rot, 0f, 1f, 40, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		float numberOfDusts = 40f;
		float rotFactor = 360f / numberOfDusts;
		for (int j = 0; (float)j < numberOfDusts; j++)
		{
			float rot2 = MathHelper.ToRadians((float)j * rotFactor);
			Vector2 offset = Utils.RotatedBy(new Vector2(15f, 0f), (double)rot2, default(Vector2));
			Vector2 velOffset = Utils.RotatedBy(new Vector2(42.5f, 0f), (double)rot2, default(Vector2)) * ((j % 2 == 0) ? 0.8f : ((j % 3 == 0) ? 0.6f : 1f));
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + offset * 6f, velOffset, "CalamityMod/Particles/SmallBloom", affectedByGravity: false, 35, 1f, Color.Magenta * 0.75f, new Vector2(1.2f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.3f));
		}
		GalaxyMetaball.SpawnParticle(base.Projectile.Center, Vector2.Zero, 275f);
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity * 0f, ModContent.ProjectileType<GalaxySmasherBlast>(), base.Projectile.damage / 2, base.Projectile.knockBack, base.Projectile.owner);
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/GalaxySmasher", (AssetRequestMode)2).Value;
		Asset<Texture2D> p2 = ModContent.Request<Texture2D>("CalamityMod/Particles/CircularSmearSmokey", (AssetRequestMode)2);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor * 0.5f, 3, texture, drawCentered: true, shrink: true);
		Vector2 generalDrawPos = base.Projectile.Center - Main.screenPosition;
		Projectile projectile = base.Projectile;
		Color val = usedColor;
		((Color)(ref val)).A = 0;
		projectile.DrawProjectileWithBackglow(val, Color.White, 9.5f, texture, null, (SpriteEffects)(base.Projectile.direction < 0));
		Texture2D value = p2.Value;
		val = usedColor;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(value, generalDrawPos, null, val * 0.65f, base.Projectile.rotation * Main.rand.NextFloat(1.4f, 1.45f), p2.Size() * 0.5f, 1.2f * Main.rand.NextFloat(0.9f, 1.1f), (SpriteEffects)0);
		return false;
	}

	public GalaxySmasherEcho()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		rotatehammer = 15f;
		usedColor = Color.Aqua;
		base._002Ector();
	}
}
