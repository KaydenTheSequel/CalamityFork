using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class DirectStrike : ModProjectile, ILocalizedModType, IModType
{
	public bool hasStongDisplacement;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public bool invalidTarget
	{
		get
		{
			if (!(base.Projectile.ai[0] < 0f))
			{
				return base.Projectile.ai[0] > 199f;
			}
			return true;
		}
	}

	public Vector2 pushVelocity
	{
		get
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(base.Projectile.ai[1], base.Projectile.ai[2]);
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 2;
		base.Projectile.height = 2;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 0;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 2;
	}

	public override void AI()
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.knockBack < 0f)
		{
			hasStongDisplacement = true;
			base.Projectile.knockBack = 0f;
		}
		if (!invalidTarget)
		{
			base.Projectile.Center = Main.npc[(int)base.Projectile.ai[0]].Center;
		}
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (invalidTarget || base.Projectile.ai[0] == (float)target.whoAmI)
		{
			return null;
		}
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		if (((Rectangle)(ref projHitbox)).Intersects(targetHitbox))
		{
			NPC target = Main.npc[(int)base.Projectile.ai[0]];
			if (pushVelocity != Vector2.Zero && pushVelocity.X < 255f && !invalidTarget && target.CanBeMoved(hasStongDisplacement))
			{
				target.velocity = pushVelocity * ((target.knockBackResist == 0f) ? 0.5f : 1f);
			}
			return true;
		}
		return false;
	}
}
