using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace CalamityMod.Projectiles.Rogue;

public class SpentFuelContainerProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/SpentFuelContainer";

	public override void SetDefaults()
	{
		base.Projectile.width = 22;
		base.Projectile.height = 34;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.aiStyle = 2;
		base.Projectile.timeLeft = 200;
		base.Projectile.tileCollide = true;
		base.Projectile.alpha = 0;
		base.AIType = 48;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		Vector2 dspeed = default(Vector2);
		for (int i = 0; i < 30; i++)
		{
			((Vector2)(ref dspeed))._002Ector(Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f));
			Dust.NewDust(base.Projectile.Center, 1, 1, 75, dspeed.X, dspeed.Y, 0, default(Color), 1.1f);
		}
		SoundEngine.PlaySound(in SoundID.Item107, base.Projectile.Bottom);
		if (WorldUtils.Find(base.Projectile.Top.ToTileCoordinates(), Searches.Chain(new Searches.Down(80), new Conditions.IsSolid()), out var result))
		{
			int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), result.ToVector2() * 16f, Vector2.Zero, ModContent.ProjectileType<SulphuricNukesplosion>(), (int)((float)base.Projectile.damage * 0.5f), 2f, base.Projectile.owner);
			if (proj.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[proj].Calamity().stealthStrike = base.Projectile.Calamity().stealthStrike;
			}
		}
	}
}
