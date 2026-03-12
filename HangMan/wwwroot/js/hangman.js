document.addEventListener("DOMContentLoaded", () => {
    const root = document.getElementById("game-root");

    const word = root.dataset.word.toLowerCase();
    let health = parseInt(root.dataset.health);

    let correct = [];
    let incorrect = [];

    const ui = {
        word: document.createElement("div"),
        health: document.createElement("div"),
        incorrect: document.createElement("div"),
        input: document.createElement("input"),
        button: document.createElement("button"),
        message: document.createElement("div")
    };

    ui.word.className = "word-display";
    ui.health.className = "health-display";
    ui.incorrect.className = "incorrect-display";
    ui.input.maxLength = 1;
    ui.input.className = "guess-input";
    ui.button.textContent = "Guess";
    ui.button.className = "guess-button";

    root.append(ui.word, ui.health, ui.incorrect, ui.input, ui.button, ui.message);

    function render() {
        // Word display
        ui.word.innerHTML = [...word]
            .map(c => correct.includes(c) ? c : "_")
            .join(" ");

        // Health
        ui.health.textContent = `Health: ${health}`;

        // Incorrect guesses
        ui.incorrect.textContent = `Incorrect: ${incorrect.join(", ")}`;
    }

    function checkWin() {
        return [...word].every(c => correct.includes(c));
    }

    function checkLoss() {
        return health <= 0;
    }

    ui.button.addEventListener("click", () => {
        const guess = ui.input.value.toLowerCase();
        ui.input.value = "";

        if (!guess.match(/[a-z]/)) return;

        if (word.includes(guess)) {
            if (!correct.includes(guess)) correct.push(guess);
        } else {
            if (!incorrect.includes(guess)) {
                incorrect.push(guess);
                health--;
            }
        }

        render();

        if (checkWin()) {
            ui.message.textContent = "You win!";
            ui.button.disabled = true;
            ui.input.disabled = true;
        }

        if (checkLoss()) {
            ui.message.textContent = `You lost! The word was: ${word}`;
            ui.button.disabled = true;
            ui.input.disabled = true;
        }
    });

    render();
});
