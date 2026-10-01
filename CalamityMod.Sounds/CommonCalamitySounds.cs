using Terraria.Audio;

namespace CalamityMod.Sounds;

public static class CommonCalamitySounds
{
	public static readonly SoundStyle AstralNPCDeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/AstralEnemyDeath")
	{
		Volume = 0.7f
	};

	public static readonly SoundStyle AstralNPCHitSound = new SoundStyle("CalamityMod/Sounds/NPCHit/AstralEnemyHit", 3);

	public static readonly SoundStyle ELRFireSound = new SoundStyle("CalamityMod/Sounds/Item/ELRFire");

	public static readonly SoundStyle ExoDeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/ExoDeath")
	{
		MaxInstances = 1
	};

	public static readonly SoundStyle ExoHitSound = new SoundStyle("CalamityMod/Sounds/NPCHit/ExoHit", 4)
	{
		Volume = 0.4f
	};

	public static readonly SoundStyle ExoLaserShootSound = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/ExoLaserShoot");

	public static readonly SoundStyle ExoPlasmaExplosionSound = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/ExoPlasmaExplosion", 2);

	public static readonly SoundStyle ExoPlasmaShootSound = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/ExoPlasmaShoot");

	public static readonly SoundStyle FlareSound = new SoundStyle("CalamityMod/Sounds/Item/FlareSound");

	public static readonly SoundStyle LargeWeaponFireSound = new SoundStyle("CalamityMod/Sounds/Item/LargeWeaponFire");

	public static readonly SoundStyle LaserCannonSound = new SoundStyle("CalamityMod/Sounds/Item/LaserCannon")
	{
		Volume = 0.85f
	};

	public static readonly SoundStyle LightningSound = new SoundStyle("CalamityMod/Sounds/Custom/LightningStrike")
	{
		Volume = 1.6f
	};

	public static readonly SoundStyle LouderPhantomPhoenix = new SoundStyle("CalamityMod/Sounds/Item/LouderPhantomPhoenix", 3);

	public static readonly SoundStyle MeatySlashSound = new SoundStyle("CalamityMod/Sounds/Custom/MeatySlash");

	public static readonly SoundStyle PlagueBoomSound = new SoundStyle("CalamityMod/Sounds/Custom/PlagueSounds/PlagueBoom", 4);

	public static readonly SoundStyle PlasmaBoltSound = new SoundStyle("CalamityMod/Sounds/Item/PlasmaBolt")
	{
		Volume = 0.8f
	};

	public static readonly SoundStyle PlatingMine = new SoundStyle("CalamityMod/Sounds/Custom/PlatingMine", 3);

	public static readonly SoundStyle ScissorGuillotineSnapSound = new SoundStyle("CalamityMod/Sounds/Custom/ScissorGuillotineSnap");

	public static readonly SoundStyle SwiftSliceSound = new SoundStyle("CalamityMod/Sounds/Custom/SwiftSlice");

	public static readonly SoundStyle VoidstoneMine = new SoundStyle("CalamityMod/Sounds/Custom/VoidstoneMine", 3)
	{
		Volume = 0.4f
	};

	public static readonly SoundStyle WulfrumNPCDeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/WulfrumDeath");

	public static readonly SoundStyle WyrmScreamSound = new SoundStyle("CalamityMod/Sounds/Custom/WyrmScream");
}
