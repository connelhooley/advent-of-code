var _ = require('underscore');
const input = process.argv.length > 2
    ? process.argv[2] 
    : 'R3, L5, R2, L1, L2, R5, L2, R2, L2, L2, L1, R2, L2, R4, R4, R1, L2, L3, R3, L1, R2, L2, L4, R4, R5, L3, R3, L3, L3, R4, R5, L3, R3, L5, L1, L2, R2, L1, R3, R1, L1, R187, L1, R2, R47, L5, L1, L2, R4, R3, L3, R3, R4, R1, R3, L1, L4, L1, R2, L1, R4, R5, L1, R77, L5, L4, R3, L2, R4, R5, R5, L2, L2, R2, R5, L2, R194, R5, L2, R4, L5, L4, L2, R5, L3, L2, L5, R5, R2, L3, R3, R1, L4, R2, L1, R5, L1, R5, L1, L1, R3, L1, R5, R2, R5, R5, L4, L5, L5, L5, R3, L2, L5, L4, R3, R1, R1, R4, L2, L4, R5, R5, R4, L2, L2, R5, R5, L5, L2, R4, R4, L4, R1, L3, R1, L1, L1, L1, L4, R5, R4, L4, L4, R5, R3, L2, L2, R3, R1, R4, L3, R1, L4, R3, L3, L2, R2, R2, R2, L1, L4, R3, R2, R2, L3, R2, L3, L2, R4, L2, R3, L4, R5, R4, R1, R5, R3';
const up = 1;
const right = 2;
const down = 3;
const left = 4;
const startingPoint = {
    direction: up,
    position: {
        x: 0, 
        y: 0
    }
};

const result = input
    .split(', ')
    .map(([first,...rest]) => ({
        turn: first,
        distance: parseInt(rest.join(''))
    }))
    .reduce((current, instruction) => {
        const direction = rotate(instruction.turn, current.direction)
        const position = move(current.position, instruction.distance, direction);
        return {
            position,
            direction
        };
    }, startingPoint);

console.log(calculateDistance(result.position));

function rotate(turn, direction) {
    return turn === 'R' 
        ? rotateRight(direction)
        : rotateLeft(direction);
}

function rotateLeft(current) {
    switch (current) {
        case up:
            return left;
        case right:
            return up;
        case down:
            return right;
        case left:
            return down;
    }
}

function rotateRight(current) {
    switch (current) {
        case up:
            return right;
        case right:
            return down;
        case down:
            return left;
        case left:
            return up;
    }
}

function move({ x, y }, distance, direction) {
    switch (direction) {
        case up:
            return {
                x,
                y: y += distance
            };
        case right:
            return {
                x: x += distance,
                y
            };
        case down:
            return {
                x,
                y: y -= distance
            };
        case left:
            return {
                x: x -= distance,
                y
            };
    }
}

function calculateDistance({x, y}) {
    return Math.abs(x) + Math.abs(y);
}