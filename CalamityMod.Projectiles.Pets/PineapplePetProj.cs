using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class PineapplePetProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Pets";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.CharacterPreviewAnimations[base.Type] = ProjectileID.Sets.SimpleLoop(0, Main.projFrames[base.Type], 6).WithOffset(-2f, -20f).WithSpriteDirection(-1)
			.WhenNotSelected(0, 0);
	}

	public override void SetDefaults()
	{
		base.Projectile.netImportant = true;
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
	}

	public override void AI()
	{
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (!player.active)
		{
			base.Projectile.active = false;
			return;
		}
		if (player.dead)
		{
			modPlayer.pineapplePet = false;
		}
		if (modPlayer.pineapplePet)
		{
			base.Projectile.timeLeft = 2;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
			base.Projectile.frameCounter = 0;
		}
		float passiveMvtFloat = 0.1f;
		base.Projectile.tileCollide = false;
		float range = 200f;
		float xDist = player.Center.X - base.Projectile.Center.X - 2f;
		float yDist = player.Center.Y - base.Projectile.Center.Y - 60f;
		Vector2 playerVector = default(Vector2);
		((Vector2)(ref playerVector))._002Ector(xDist, yDist);
		float playerDist = ((Vector2)(ref playerVector)).Length();
		float returnSpeed = 7f;
		if (playerDist < range && player.velocity.Y == 0f && base.Projectile.position.Y + (float)base.Projectile.height <= player.position.Y + (float)player.height && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
		{
			base.Projectile.ai[0] = 0f;
			if (base.Projectile.velocity.Y < -6f)
			{
				base.Projectile.velocity.Y = -6f;
			}
		}
		if (playerDist > 2000f)
		{
			base.Projectile.position.X = player.Center.X - (float)(base.Projectile.width / 2);
			base.Projectile.position.Y = player.Center.Y - (float)(base.Projectile.height / 2);
			base.Projectile.netUpdate = true;
		}
		if (playerDist < 4f)
		{
			base.Projectile.velocity.X = xDist;
			base.Projectile.velocity.Y = yDist;
			passiveMvtFloat = 0f;
		}
		else
		{
			if (playerDist > 350f)
			{
				passiveMvtFloat = 0.2f;
				returnSpeed = 12f;
			}
			float speedMult = returnSpeed / playerDist;
			xDist *= speedMult;
			yDist *= speedMult;
		}
		if (base.Projectile.velocity.X < xDist)
		{
			base.Projectile.velocity.X += passiveMvtFloat;
			if (base.Projectile.velocity.X < 0f)
			{
				base.Projectile.velocity.X += passiveMvtFloat;
			}
		}
		if (base.Projectile.velocity.X > xDist)
		{
			base.Projectile.velocity.X -= passiveMvtFloat;
			if (base.Projectile.velocity.X > 0f)
			{
				base.Projectile.velocity.X -= passiveMvtFloat;
			}
		}
		if (base.Projectile.velocity.Y < yDist)
		{
			base.Projectile.velocity.Y += passiveMvtFloat;
			if (base.Projectile.velocity.Y < 0f)
			{
				base.Projectile.velocity.Y += passiveMvtFloat;
			}
		}
		if (base.Projectile.velocity.Y > yDist)
		{
			base.Projectile.velocity.Y -= passiveMvtFloat;
			if (base.Projectile.velocity.Y > 0f)
			{
				base.Projectile.velocity.Y -= passiveMvtFloat;
			}
		}
		base.Projectile.direction = -player.direction;
		base.Projectile.spriteDirection = 1;
		base.Projectile.rotation = base.Projectile.velocity.Y * 0.05f * (float)(-base.Projectile.direction);
	}
}
