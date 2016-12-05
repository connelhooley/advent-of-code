const _ = require('underscore');

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
    },
    isFinished: false,
    journey: [
        {
            x:0,
            y:0
        }
    ]
};

const destination = input
    .split(', ')
    .map(formatInstruction)
    .reduce(performJourney, startingPoint)
    .position;

console.log(calculateDistance(destination));

function formatInstruction([first,...rest]) {
    return {
        turn: first,
        distance: parseInt(rest.join(''))
    };
}

function performJourney(current, instruction) {
    if (current.isFinished) return current;
    const direction = rotate(instruction.turn, current.direction)
    const positions = move(current.position, instruction.distance, direction);
    const duplicate = findDuplicate(positions, current.journey);
    const journey = current.journey.concat(positions);
    if (duplicate !== undefined) {
        return {
            position: duplicate,
            isFinished: true,
            direction,
            journey
        };
    } else {
        return {
            position: _.last(positions),
            isFinished: false,
            direction,
            journey
        };
    }
}

function rotate(turn, direction) {
    return isRight(turn) 
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

function move(postion, distance, direction) {
    return getSteps(postion, distance, direction)
        .map(isHorizontal(direction) 
            ? mapHorizontally(postion)
            : mapVertically(postion));
}

function mapHorizontally({y}) {
    return step => ({
        x: step,
        y
    });
}

function mapVertically({x}) {
    return step => ({
        x,
        y: step
    });
}

function getSteps({x, y}, distance, direction) {
    switch (direction) {
        case up:
            return range(y+1, y + distance);
        case right:
            return range(x+1, x + distance);
        case down:
            return range(y-1, y - distance);
        case left:
            return range(x-1, x - distance);
    }
}

function findDuplicate(positions, journey) {
    return positions.find(position => journey.find(i => comparePositions(position, i)));
}

function isRight(turn) {
    return turn === 'R';
}

function isHorizontal(direction) {
    return direction === left || direction === right;
}

function comparePositions({x: x1, y: y1}, {x: x2, y: y2}) {
    return x1 === x2 && y1 === y2;
}

function calculateDistance({x, y}) {
    return Math.abs(x) + Math.abs(y);
}

function range(start, end) {
    return start < end 
        ? _.range(start, end+1, 1)
        : _.range(start, end-1, -1);
}