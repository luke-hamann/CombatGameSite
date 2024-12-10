let audio = document.getElementById("myAudio");

// When page is loaded, play music
document.addEventListener("DOMContentLoaded", () => {
    audio.play();
});

// Check if there is a saved state in sessionStorage
if (sessionStorage.getItem('audioState')) {
    const savedState = JSON.parse(sessionStorage.getItem('audioState'));

    // Restore the audio state
    audio.currentTime = savedState.currentTime;
    if (savedState.isPlaying) {
        audio.play();
    }
}

// Save the audio state before the page unloads
window.addEventListener('beforeunload', () => {
    sessionStorage.setItem('audioState', JSON.stringify({
        currentTime: audio.currentTime,
        isPlaying: !audio.paused
    }));
});
