using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class SystemBaneLightning : ModProjectile, ILocalizedModType, IModType
{
	public const float OuterLightningScale = 0.5f;

	public const float InnerLightningScale = 0.3f;

	public const float OuterLightningOpacity = 0.35f;

	public const float InnerLightningOpacity = 0.75f;

	public static readonly Color OuterLightningColor;

	public static readonly Color InnerLightningColor;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/LightningProj";

	public int ElectrocutionTarget
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

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 60;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 22;
		base.Projectile.height = 22;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 40 * (base.Projectile.extraUpdates + 1);
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 16;
	}

	public override void AI()
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			ElectrocutionTarget = -1;
			base.Projectile.localAI[0] = 1f;
		}
		bool hasSelectedTarget = ElectrocutionTarget >= 0 && ElectrocutionTarget < Main.npc.Length;
		NPC potentialTarget = base.Projectile.Center.ClosestNPCAt(800f);
		if (hasSelectedTarget)
		{
			potentialTarget = Main.npc[ElectrocutionTarget];
		}
		if (potentialTarget != null)
		{
			if (hasSelectedTarget && base.Projectile.Distance(potentialTarget.Center) < 14f)
			{
				base.Projectile.velocity = Vector2.Zero;
			}
			else
			{
				ArcToTarget(potentialTarget);
			}
		}
		if (Main.rand.NextBool(10))
		{
			base.Projectile.velocity = base.Projectile.velocity.RotatedByRandom(0.5199999809265137);
			base.Projectile.netUpdate = true;
		}
	}

	public void ArcToTarget(NPC target)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		float updatedVelocityDirection = base.Projectile.velocity.ToRotation().AngleTowards(base.Projectile.AngleTo(target.Center), 0.25f);
		base.Projectile.velocity = updatedVelocityDirection.ToRotationVector2() * ((Vector2)(ref base.Projectile.velocity)).Length();
		if (Main.rand.NextBool(5))
		{
			base.Projectile.velocity = base.Projectile.velocity.RotatedByRandom(0.8999999761581421);
			base.Projectile.ForceNetUpdate();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		if (ElectrocutionTarget == -1)
		{
			ElectrocutionTarget = target.whoAmI;
			base.Projectile.velocity = Vector2.Zero;
			base.Projectile.netUpdate = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		List<Vector2> oldPositions = base.Projectile.oldPos.Where(delegate(Vector2 oldPosition)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return oldPosition != Vector2.Zero;
		}).ToList();
		for (int i = 0; i < oldPositions.Count - 1; i++)
		{
			DelegateMethods.f_1 = 0.35f;
			DelegateMethods.c_1 = OuterLightningColor;
			Vector2 start = oldPositions[i] + base.Projectile.Size * 0.5f - Main.screenPosition;
			Vector2 end = oldPositions[i + 1] + base.Projectile.Size * 0.5f - Main.screenPosition;
			Utils.DrawLaser(Main.spriteBatch, TextureAssets.Projectile[base.Type].Value, start, end, new Vector2(0.5f), DelegateMethods.LightningLaserDraw);
			DelegateMethods.f_1 = 0.75f;
			DelegateMethods.c_1 = InnerLightningColor;
			Utils.DrawLaser(Main.spriteBatch, TextureAssets.Projectile[base.Type].Value, start, end, new Vector2(0.3f), DelegateMethods.LightningLaserDraw);
		}
		return false;
	}

	static SystemBaneLightning()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		OuterLightningColor = Color.Cyan;
		InnerLightningColor = Color.White;
	}
}
