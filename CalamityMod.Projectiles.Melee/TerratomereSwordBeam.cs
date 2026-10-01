using System;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class TerratomereSwordBeam : ModProjectile, ILocalizedModType, IModType
{
	public int TargetIndex = -1;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 136;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 135;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.extraUpdates = 1;
	}

	public override void AI()
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 400f, 24f, 20f);
		base.Projectile.scale = Utils.GetLerpValue(0f, 8f, base.Projectile.timeLeft, clamped: true);
		Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(5) ? 131 : 294, -base.Projectile.velocity * Main.rand.NextFloat(0.05f, 0.3f));
		dust.noGravity = true;
		dust.scale = Main.rand.NextFloat(0.75f, 0.95f);
		if (dust.type == 131)
		{
			dust.scale = Main.rand.NextFloat(0.55f, 0.75f);
		}
		else
		{
			dust.fadeIn = 0.5f;
		}
	}

	public float SlashWidthFunction(float _, Vector2 vertexPos)
	{
		return (float)base.Projectile.width * base.Projectile.scale * Utils.GetLerpValue(0f, 0.1f, _, clamped: true);
	}

	public Color SlashColorFunction(float _, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.Turquoise;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		TargetIndex = target.whoAmI;
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<TerratomereExplosion>(), (int)((float)base.Projectile.damage * 0.5f), base.Projectile.knockBack, base.Projectile.owner);
		if (base.Projectile.timeLeft > 12)
		{
			base.Projectile.timeLeft = 12;
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.2f;
		base.Projectile.damage = 0;
		base.Projectile.netUpdate = true;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner && TargetIndex >= 0)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Main.npc[TargetIndex].Center, Vector2.Zero, ModContent.ProjectileType<TerratomereSlashCreator>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, TargetIndex, Main.rand.NextFloat((float)Math.PI * 2f));
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:ExobladePierce"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/BlobbyNoise", (AssetRequestMode)2));
		GameShaders.Misc["CalamityMod:ExobladePierce"].UseImage2("Images/Extra_189");
		GameShaders.Misc["CalamityMod:ExobladePierce"].UseColor(Terratomere.TerraColor1);
		GameShaders.Misc["CalamityMod:ExobladePierce"].UseSecondaryColor(Terratomere.TerraColor2);
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(SlashWidthFunction, SlashColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ExobladePierce"]), 30);
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.oldPos[0] == Vector2.Zero)
		{
			return false;
		}
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.oldPos[0] + base.Projectile.Size * 0.5f, base.Projectile.Center);
	}
}
