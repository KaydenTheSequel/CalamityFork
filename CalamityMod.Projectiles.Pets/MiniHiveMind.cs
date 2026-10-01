using CalamityMod.Buffs.Pets;
using CalamityMod.CalPlayer;
using CalamityMod.NPCs.HiveMind;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class MiniHiveMind : ModProjectile, ILocalizedModType, IModType
{
	private int reelBackCooldown;

	private int charging;

	public new string LocalizationCategory => "Projectiles.Pets";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 16;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.CharacterPreviewAnimations[base.Type] = ProjectileID.Sets.SimpleLoop(0, Main.projFrames[base.Type], 6).WithOffset(-12f, 0f).WithSpriteDirection(-1)
			.WhenNotSelected(0, 0);
	}

	public override void SetDefaults()
	{
		base.Projectile.netImportant = true;
		base.Projectile.width = 38;
		base.Projectile.height = 44;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (!player.active)
		{
			base.Projectile.active = false;
			return;
		}
		if (!player.HasBuff(ModContent.BuffType<MiniMindBuff>()))
		{
			base.Projectile.Kill();
		}
		if (player.dead)
		{
			modPlayer.hiveMindPet = false;
		}
		if (modPlayer.hiveMindPet)
		{
			base.Projectile.timeLeft = 2;
		}
		if (charging <= 0)
		{
			base.Projectile.FloatingPetAI(faceRight: true, 0.05f);
		}
		Vector2 playerVec = player.Center - base.Projectile.Center;
		float playerDist = ((Vector2)(ref playerVec)).Length();
		if (reelBackCooldown > 0)
		{
			reelBackCooldown--;
		}
		if (charging > 0)
		{
			charging--;
		}
		if (reelBackCooldown <= 0 && Main.rand.NextBool(500) && playerDist < 100f && charging <= 0 && Main.myPlayer == base.Projectile.owner)
		{
			reelBackCooldown = 300;
			((Vector2)(ref playerVec)).Normalize();
			base.Projectile.velocity = playerVec * 8f;
			charging = 50;
			SoundStyle style = HiveMind.FastRoarSound with
			{
				Volume = SoundID.ForceRoar.Volume * 0.5f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.netUpdate = true;
		}
		if (charging < 22 && charging > 0)
		{
			base.Projectile.alpha += 12;
		}
		if (charging == 1)
		{
			float xOffset = Main.rand.NextFloat(400f, 600f) * (Main.rand.NextBool() ? (-1f) : 1f);
			float yOffset = Main.rand.NextFloat(400f, 600f) * (Main.rand.NextBool() ? (-1f) : 1f);
			Vector2 teleportPos = default(Vector2);
			((Vector2)(ref teleportPos))._002Ector(player.Center.X + xOffset, player.Center.Y + yOffset);
			base.Projectile.Center = teleportPos;
			base.Projectile.alpha = 255;
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.alpha > 0 && charging <= 0)
		{
			base.Projectile.alpha -= 12;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		if (base.Projectile.alpha > 255)
		{
			base.Projectile.alpha = 255;
		}
		if (charging > 0)
		{
			base.Projectile.rotation.AngleTowards(0f, 0.1f);
		}
		if (base.Projectile.frameCounter++ % 6 == 0)
		{
			base.Projectile.frame++;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
	}
}
