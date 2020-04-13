# Playground Challenge

## Goals

**Requirements:**

- No violence
- Multiplayer cooperative on same keyboard where players have different abilities / roles
- 3 levels (or endless)
- No external assets – using only assets from Playground

**Evaluation Criteria:**

- Ease of play –playable with no instructions (on screen text or limited intro text is OK)
- Interdependency – neither player should be able to advance or win by themselves
- Execution – Looks and feels like a game, sufficiently different from examples
- Creativity – Unique is good, as long as it is executed well

## Reflection

This project was a roller coaster of learning Unity and debugging throughout. I spent almost exactly 20 hours building a game with 1 minute of gameplay.

I did not modify existing scenes in the Playground asset package, but instead constructed scenes and NPC prefabs from scratch.

Major takeaways:

* Design scripts to be small and highly modular
* Avoid using `OnTriggerStay2D()` to detect I/O events, due to the timing interval being [chosen randomly](https://stackoverflow.com/a/44684696); use `Update()` instead
* Be careful of feature creep when deadlines are involved
* Don't be afraid to ask for help (thanks Emily <3)
* Dig deeper into camera controls, programmatic instantiation of GameObjects, and coroutines

## [Play Now](./release/index.html)
