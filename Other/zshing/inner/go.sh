#!/bin/zsh

echo "in inner/go"
zsh $(dirname ${(%):-%N})/../inner2/go2.sh

