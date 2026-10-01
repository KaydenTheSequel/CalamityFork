using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class Honeycomb : ModProjectile, ILocalizedModType, IModType
{
	private const float radius = 15f;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/HardenedHoneycomb";

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		base.Projectile.ai[0]++;
		base.Projectile.rotation += MathHelper.ToRadians(base.Projectile.velocity.X * 1.25f);
		if (base.Projectile.ai[0] > 45f)
		{
			base.Projectile.velocity.X *= 0.97f;
			base.Projectile.velocity.Y += 0.28f;
			if (base.Projectile.velocity.Y > 18f)
			{
				base.Projectile.velocity.Y = 18f;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		Player player = Main.LocalPlayer;
		if (base.Projectile.Calamity().stealthStrike)
		{
			player.AddBuff(48, 600);
		}
		SpawnProjectiles();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		Player player = Main.LocalPlayer;
		if (base.Projectile.Calamity().stealthStrike)
		{
			player.AddBuff(48, 600);
		}
		SpawnProjectiles();
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCDeath1, base.Projectile.position);
		SpawnProjectiles();
		for (int dust_splash = 0; dust_splash < 9; dust_splash++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 9, (0f - base.Projectile.velocity.X) * 0.15f, (0f - base.Projectile.velocity.Y) * 0.15f, 159, default(Color), 1.5f);
		}
	}

	public void SpawnProjectiles()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] == 1f)
		{
			return;
		}
		base.Projectile.ai[1] = 1f;
		Player player = Main.LocalPlayer;
		int fragAmt = 2;
		for (int i = 0; i < fragAmt; i++)
		{
			Vector2 shardVelocity = CalamityUtils.RandomVelocity(100f, 35f, 55f);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, shardVelocity, ModContent.ProjectileType<HoneycombFragment>(), (int)((float)base.Projectile.damage * 0.8f), base.Projectile.knockBack, base.Projectile.owner, Main.rand.Next(3));
		}
		if (base.Projectile.Calamity().stealthStrike)
		{
			int beeAmt = 4;
			for (int j = 0; j < beeAmt; j++)
			{
				Vector2 beeVelocity = CalamityUtils.RandomVelocity(100f, 35f, 55f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, beeVelocity, player.beeType(), player.beeDamage((int)((float)base.Projectile.damage * 0.8f)), player.beeKB(0.25f), player.whoAmI);
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
